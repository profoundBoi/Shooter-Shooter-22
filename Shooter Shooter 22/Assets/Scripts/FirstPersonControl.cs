using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Windows;

public class FirstPersonControl : MonoBehaviour
{
    [Header("MOVEMENT SETTINGS")]
    [Space(5)]
    // Public variables to set movement and look speed, and the player camera
    public float moveSpeed; // Speed at which the player moves
    public float lookSpeed; // Sensitivity of the camera movement
    public float gravity = -9.81f; // Gravity value
    public float jumpHeight = 1.0f; // Height of the jump
    public Transform playerCamera; // Reference to the player's camera
    // Private variables to store input values and the character controller
    private Vector2 moveInput; // Stores the movement input from the player
    private Vector2 lookInput; // Stores the look input from the player
    private float verticalLookRotation = 0f; // Keeps track of vertical camera rotation for clamping
    private Vector3 velocity; // Velocity of the player
    private CharacterController characterController; // Reference to the CharacterController component

    [Header("WALL RUN SETTINGS")]
    [Space(5)]
    public float wallRunSpeed = 8f; // Speed while wall running
    public float wallRunDuration = 2f; // How long the player can wall run
    public float wallGravity = -2f; // Gravity while wall running
    private bool isWallRunning = false;
    private float wallRunTime;
    private Vector3 wallNormal;

    [Header("SHOOTING SETTINGS")]
    [Space(5)]
    public GameObject projectilePrefab; // Projectile prefab for shooting
    public Transform firePoint; // Point from which the projectile is fired
    public float projectileSpeed = 20f; // Speed at which the projectile is fired


    [Header("PICKING UP SETTINGS")]
    [Space(5)]
    public Transform holdPosition; // Position where the picked-up object will be held
    private GameObject heldObject; // Reference to the currently held object
    public float pickUpRange = 3f; // Range within which objects can be picked up
    private bool holdingGun = false;

    [Header("CROUCH SETTINGS")]
    [Space(5)]
    public float crouchHeight = 1.0f; //make short
    public float standingHeight = 2.0f; //make normal
    public float crouchSpeed = 1.5f; //make slow
    private bool isCrouching = false; //chech if crouch

   



    public void Shoot()
    {
        if (holdingGun == true)
        {


            // Instantiate the projectile at the fire point
            GameObject projectile = Instantiate(projectilePrefab,
            firePoint.position, firePoint.rotation);

            // Get the Rigidbody component of the projectile and set its velocity
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * projectileSpeed;

            // Destroy the projectile after 3 seconds
            Destroy(projectile, 3f);
        }
    }
         
    private void Awake()
    {
        // Get and store the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        // Create a new instance of the input actions
        var playerInput = new Controls();

        // Enable the input actions
        playerInput.Player.Enable();

        // Subscribe to the movement input events
        playerInput.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed
        playerInput.Player.Movement.canceled += ctx => moveInput = Vector2.zero; // Reset moveInput when movement input is canceled

        // Subscribe to the look input events
        playerInput.Player.LookAround.performed += ctx => lookInput = ctx.ReadValue<Vector2>(); // Update lookInput when look input is performed
        playerInput.Player.LookAround.canceled += ctx => lookInput =  Vector2.zero; // Reset lookInput when look input is canceled

        // Subscribe to the jump input event
        playerInput.Player.Jump.performed += ctx => Jump(); // Call the Jump method when jump input is performed

        // Subscribe to the shoot input event
        playerInput.Player.Shoot.performed += ctx => Shoot(); // Call the Shoot method when shoot input is performed

        // Subscribe to the pick-up input event
        playerInput.Player.PickUp.performed += ctx => PickUpObject(); //Call the PickUpObject method when pick-up input is performed

        // subscribe to the crouch input event
        playerInput.Player.Crouch.performed += ctx => ToggleCrouch(); //Call the ToggleCrouch method when Crouch input is performed


    }
    private void Update()
    {
        // Call Move and LookAround methods every frame to handle player movement and camera rotation
        Move();
        LookAround();
        ApplyGravity();
        HandleWallRun();

    }
    public void Move()
    {
        // Create a movement vector based on the input
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        // Transform direction from local to world space
        move = transform.TransformDirection(move);

        // Move the character controller based on the movement vector and speed
        characterController.Move(move * moveSpeed * Time.deltaTime);

        float currentSpeed;
        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        if (isWallRunning)
        {
            move = Vector3.Cross(wallNormal, Vector3.up).normalized * moveInput.y;
            characterController.Move(move * wallRunSpeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(move * currentSpeed * Time.deltaTime);
        }
    }

    public void ToggleCrouch()
    {
        if (isCrouching)
        {
            characterController.height = standingHeight;
            isCrouching = false;
        }
        else
        {
            characterController.height = crouchHeight;
            isCrouching = true;
        }
    }
    public void LookAround()
    {
        // Get horizontal and vertical look inputs and adjust based on sensitivity
        float LookX = lookInput.x * lookSpeed;
        float LookY = lookInput.y * lookSpeed;

        // Horizontal rotation: Rotate the player object around the y-axis
        transform.Rotate(0, LookX, 0);

        // Vertical rotation: Adjust the vertical look rotation and clamp it to prevent flipping
        verticalLookRotation -= LookY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        // Apply the clamped vertical rotation to the player camera
        playerCamera.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
    }
    public void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f; // Small value to keep the player grounded
        }
        velocity.y += gravity * Time.deltaTime; // Apply gravity to the velocity

        if (!isWallRunning)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = wallGravity;
        }

        characterController.Move(velocity * Time.deltaTime); // Apply the velocity to the character
       
    }
    public void Jump()
    {
        if (characterController.isGrounded)
        {
            // Calculate the jump velocity
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (isWallRunning)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isWallRunning = false;
        }
    }

    private void HandleWallRun()
    {
        if (isWallRunning)
        {
            wallRunTime -= Time.deltaTime;
            if (wallRunTime <= 0)
            {
                isWallRunning = false;
            }
        }
        else
        {
            CheckForWall();
        }
    }

    private void CheckForWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.right, out hit, 1f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                StartWallRun(hit.normal);
            }
        }
        else if (Physics.Raycast(transform.position, -transform.right, out hit, 1f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                StartWallRun(hit.normal);
            }
        }
    }

    private void StartWallRun(Vector3 normal)
    {
        isWallRunning = true;
        wallRunTime = wallRunDuration;
        wallNormal = normal;
        velocity.y = 0;
    }

    public void PickUpObject()
    {
        // Check if we are already holding an object
        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
            heldObject.transform.parent = null;
            holdingGun = false;
        }

        // Perform a raycast from the camera's position forward
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Debugging: Draw the ray in the Scene view
        Debug.DrawRay(playerCamera.position, playerCamera.forward *
        pickUpRange, Color.red, 2f);

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            // Check if the hit object has the tag "PickUp"
            if (hit.collider.CompareTag("PickUp"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;// Disable physics

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;
                holdingGun = true;
            }
            else if (hit.collider.CompareTag("Gun"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;// Disable physics

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;
                holdingGun = true;
            }
        }
    }
}


