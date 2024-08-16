using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Presets;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
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
        if (holdingGun == true && Ammo > 0)
        {
            Ammo--;

            // Instantiate the projectile at the fire point
            GameObject projectile = Instantiate(projectilePrefab,
            firePoint.position, firePoint.rotation);

            // Get the Rigidbody component of the projectile and set its velocity
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * projectileSpeed;

            // Destroy the projectile after 3 seconds
            Destroy(projectile, 3f);


        }

        if (holdingKnife)
        {
            // Instantiate the projectile at the fire point
            GameObject projectile = Instantiate(knifeProjectile,
            knifeSpawnPoint.position, knifeSpawnPoint.rotation);

            // Get the Rigidbody component of the projectile and set its velocity
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = knifeSpawnPoint.forward * projectileSpeed;

            // Destroy the projectile after 3 seconds
            Destroy(projectile, 10f);
            KnifeCount--;


            StartCoroutine(Throw());


        }





    }

    IEnumerator Throw()
    {
        anim.SetBool("Throw", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Throw", false);


    }

    private int KnifeCount = 3;
    public Transform knifeSpawnPoint;
    public GameObject knifeProjectile;
    public TextMeshProUGUI ammoText;
    private bool CanReload = false;
    public void Reload()
    {
        if (Ammo < 10 && CanReload)
        {
            Ammo = 10;  
            CanReload = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.CompareTag ("Ammo")) 
        { 
            Destroy(hit.gameObject);
            CanReload = true;
        }
    }

    private void Awake()
    {
        // Get and store the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();
        //gunAim.SetActive(false);
        // pickUpAim.SetActive(true);

        ammoText.text = "";

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
        playerInput.Player.LookAround.canceled += ctx => lookInput = Vector2.zero; // Reset lookInput when look input is canceled

        // Subscribe to the jump input event
        playerInput.Player.Jump.performed += ctx => Jump(); // Call the Jump method when jump input is performed

        // Subscribe to the shoot input event
        playerInput.Player.Shoot.performed += ctx => Shoot(); // Call the Shoot method when shoot input is performed


        playerInput.Player.Reload.performed += ctx => Reload();
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

        if (!holdingKnife && holdingGun == false)
        {

            gunAim.SetActive(false);
            pickUpAim.SetActive(true);
        }



        

        if (Ammo > 0 && holdingGun)
        {
            ammoText.text = "";

        }
        else if (Ammo == 0 && holdingGun && CanReload)
        {
            ammoText.text = "Click R/Triangle to reload Ammo";
        }
        else if (Ammo == 0 && holdingGun && !CanReload)
        {
            ammoText.text = "No More Ammo, Look for Ammo at the shooting Range Floor";
        }

        if (!holdingGun)
        {
            ammoText.text = "";

        }

        if (holdingKnife && KnifeCount == 0)
        {
            holdingKnife = false;
            Destroy(heldObject);
            KnifeCount = 3;
        }

        



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

        characterController.Move(velocity * Time.deltaTime); // Apply the velocity to the character

    }
    public void Jump()
    {
        if (characterController.isGrounded)
        {
            // Calculate the jump velocity
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    private bool holdingKnife = false;
    public Animator anim;
    public void PickUpObject()
    {
        // Check if we are already holding an object
        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
            heldObject.transform.parent = null;
            holdingGun = false;
        }

        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
            heldObject.transform.parent = null;
            holdingKnife = false;
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
            if (hit.collider.CompareTag("Knife"))
            {

                gunAim.SetActive(true);
                pickUpAim.SetActive(false);
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;// Disable physics


                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;
                holdingKnife = true;

            }
            else if (hit.collider.CompareTag("Gun"))
            {
                gunAim.SetActive(true);
                pickUpAim.SetActive(false);
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;// Disable physics

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.eulerAngles = new Vector3(holdPosition.eulerAngles.x, holdPosition.eulerAngles.y, holdPosition.eulerAngles.z);
                heldObject.transform.parent = holdPosition;
                holdingGun = true;

            }

        }
    }

    public GameObject gunAim;
    public GameObject pickUpAim;
    public Vector3 GunRotation;

    public int Ammo = 10;
    public GameObject ammoPrefab;
}

