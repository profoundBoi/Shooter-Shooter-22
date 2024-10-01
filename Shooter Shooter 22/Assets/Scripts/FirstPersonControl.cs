using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;


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
    public Transform holdPosition2;
    public Transform sythHoldingPosition;
    private GameObject heldObject; // Reference to the currently held object
    private GameObject heldObject2;
    public float pickUpRange = 10f; // Range within which objects can be picked up
    private bool holdingGun = false;
    private bool holdingSyth = false;   

    [Header("CROUCH SETTINGS")]
    [Space(5)]
    public float crouchHeight = 1.0f; //make short
    public float standingHeight = 2.0f; //make normal
    public float crouchSpeed = 1.5f; //make slow
    private bool isCrouching = false; //chech if crouch

    [Header("INTERACT SETTINGS")]
    [Space(5)]
    public Material switchMaterial; // Material to apply when switch is activated
    public GameObject[] objectsToChangeColor; // Array of objects to change color

    public void FlashOnAndOff()
    {
        if (holdingFlash == true && !FlashLight.activeSelf)
        {
            FlashLight.SetActive(true);
        }
        else if (holdingFlash && FlashLight.activeSelf)
        {
            FlashLight.SetActive(false);
        }
    }
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
            StartCoroutine(Throw());


        }

        

        if (holdingSyth)
        {
            StartCoroutine(Slice());
        }

        if (holdingBottle)
        {
            
            GameObject Bottles = Instantiate(Bottle, holdPosition.position, Quaternion.identity);
            Bottles.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            Rigidbody bt = Bottles.GetComponent<Rigidbody>();
            bt.velocity = bottlShootP.forward * 10;
            
            foreach (Transform child in Bottles.transform)
            {
                Rigidbody Gp = child.GetComponent<Rigidbody>();
                Gp.velocity = bottlShootP.forward * projectileSpeed;
            }
           holdingBottle = false;
            Destroy(heldObject);

        }





    }
    public GameObject Bottle;
    public Animator Sliced;
    public Transform bottlShootP;

    public GameObject FlashLight;

    public Collider Knifecollider;
    public Collider SythCollider;
    IEnumerator Slice()
    {
        Sliced.SetBool("Slice", true);
        SythCollider.isTrigger = true;

        yield return new WaitForSeconds(0.1f);
        Sliced.SetBool("Slice", false);
        SythCollider.isTrigger = false;



    }
    IEnumerator Throw()
    {
        anim.SetBool("Stab", true);
        Knifecollider.isTrigger = true;

        yield return new WaitForSeconds(0.11f);
        anim.SetBool("Stab", false);
        Knifecollider.isTrigger = false;



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
    private GameObject Gun;
    private void Awake()
    {
        // Get and store the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();
        //gunAim.SetActive(false);
        // pickUpAim.SetActive(true);

        ammoText.text = "";
        Gun = GameObject.FindGameObjectWithTag("Gun");

        //NoKey = GameObject.FindGameObjectsWithTag("NoKey");
        //Key = GameObject.FindGameObjectsWithTag("Key");

        passKey.SetActive(false);
        FlashLight.SetActive(false);

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

        // Subscribe to the interact input event
        playerInput.Player.Interact.performed += ctx => Interact(); // Interact with switch

        // Subscribe to the FlashOn input event
        playerInput.Player.FlashOn.performed += ctx => FlashOnAndOff(); // Turn flash on and off


    }
    public GameObject[] safeCode;
    public GameObject[] unsafeCode;

    //Check the colour of the material is Green
    bool OpenSafe()
    {
        foreach (GameObject go in safeCode)
        {
            Renderer Un = go.GetComponent<Renderer>();
            if (Un == null || Un.material.color != Color.green)
            {
                return false ;
            }

        }
        return true;
    }
    [Header("MessageText")]
    public TextMeshProUGUI Message;
    private void Update()
    {
        // Call Move and LookAround methods every frame to handle player movement and camera rotation
        Move();
        LookAround();
        ApplyGravity();

        if (!holdingFlash)
        {
            FlashLight.SetActive(false);
        }

        if (Open1)
        {
            Drawer.transform.position = Vector3.MoveTowards(Drawer.transform.position, Opened.position, 3 * Time.deltaTime);
        }
        if (Open2)
        {
            Drawer2.transform.position = Vector3.MoveTowards(Drawer2.transform.position, Opened2.position, 3 * Time.deltaTime);
        }
        if (Open3)
        {
            Drawer3.transform.position = Vector3.MoveTowards(Drawer3.transform.position, Opened3.position, 3 * Time.deltaTime);
        }

            if (characterController.isGrounded)
        {
            JumpsLeft = 1;
            jumpsPerformed = 0;
        }

        foreach (GameObject go in unsafeCode)
        {
            Renderer Un = go.GetComponent<Renderer>();
            if (Un != null && Un.material.color == Color.green)
            {

                foreach (GameObject obj in safeCode)
                {
                    Renderer renderer = obj.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = Color.red; // Set the color to match the switch material color
                    }
                }

                foreach (GameObject obj in unsafeCode)
                {
                    Renderer renderer = obj.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = Color.red; // Set the color to match the switch material color
                    }
                }
            }
        }
        if (OpenSafe())
        {
            Safe.SetBool("Open", true);
        }
        

       
       

        if (!holdingGun)
        {
            MeshCollider MC = Gun.GetComponent<MeshCollider>();
            MC.isTrigger = false;
        }else
        {
            MeshCollider MC = Gun.GetComponent<MeshCollider>();
            MC.isTrigger= true;
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

    private bool holdingFlash = false;
    public void Jump()
    {
        if (characterController.isGrounded )
        {
            // Calculate the jump velocity
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    private int JumpsLeft = 1;
    private int jumpsPerformed = 0;
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

        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
            heldObject.transform.parent = null;
            holdingBottle = false;
        }

        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
            heldObject.transform.parent = null;
            holdingSyth = false;
        }

       


        // Perform a raycast from the camera's position forward
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Debugging: Draw the ray in the Scene view
        Debug.DrawRay(playerCamera.position, playerCamera.forward * pickUpRange, Color.red, 10f);

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            // Check if the hit object has the tag "PickUp"
            if (hit.collider.CompareTag("Knife"))
            {

               
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;// Disable physics


                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;
                holdingKnife = true;
                
            }

            // Check if the hit object has the tag "PickUp"
            else if (hit.collider.CompareTag("Syth"))
            {

               
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;// Disable physics


                // Attach the object to the hold position
                heldObject.transform.position = sythHoldingPosition.position;
                heldObject.transform.rotation = sythHoldingPosition.rotation;
                heldObject.transform.parent = sythHoldingPosition;
                holdingSyth = true;
         
            }
            else if (hit.collider.CompareTag("Gun"))
            {
                
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;// Disable physics

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.eulerAngles = new Vector3(holdPosition.eulerAngles.x, holdPosition.eulerAngles.y, holdPosition.eulerAngles.z);
                heldObject.transform.parent = holdPosition;
                holdingGun = true;
    
            }

            else if (hit.collider.CompareTag("Flash"))
            {

                // Pick up the object
                heldObject2 = hit.collider.gameObject;
                heldObject2.GetComponent<Rigidbody>().isKinematic = true;// Disable physics

                // Attach the object to the hold position
                heldObject2.transform.position = holdPosition2.position;
                heldObject2.transform.eulerAngles = new Vector3(holdPosition2.eulerAngles.x + 90, holdPosition2.eulerAngles.y, holdPosition2.eulerAngles.z);
                heldObject2.transform.parent = holdPosition2;
                holdingFlash = true;
                StartCoroutine(FlashLightOn());

            }

            else if (hit.collider.CompareTag("PickUp"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.tag = "Bottle";
                heldObject.GetComponent<Rigidbody>().isKinematic = true;// Disable physics

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.eulerAngles = new Vector3(holdPosition.eulerAngles.x, holdPosition.eulerAngles.y, holdPosition.eulerAngles.z);
                heldObject.transform.parent = holdPosition;
                holdingBottle = true;
           

            }
            else if (hit.collider.CompareTag("PassKey"))
            {
                StartCoroutine(PassKey());

            }


        }
    }

    IEnumerator FlashLightOn ()
    {
        yield return new WaitForSeconds(0);
        ammoText.text = "RIght Click to Turn on and off";
        yield return new WaitForSeconds(4);
        ammoText.text = "";
    }

    IEnumerator PassKey()
    {
        yield return new WaitForSeconds (0);
        passKey.SetActive (true);
        yield return new WaitForSeconds (3);
        passKey.SetActive(false);
    }

    public Vector3 GunRotation;
    private bool holdingBottle = false;

    public int Ammo = 10;
    public GameObject ammoPrefab;
    public GameObject passKey;
    public void Interact()
    {
        // Perform a raycast to detect the lightswitch
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            if (hit.collider.CompareTag("Switch")) // Assuming the switch has this tag
            {
                // Change the material color of the objects in the array
                foreach (GameObject obj in objectsToChangeColor)
                {
                    Renderer renderer = obj.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = switchMaterial.color; // Set the color to match the switch material color
                    }
                }
            }
            else if (hit.collider.CompareTag("Door")) // Check if the object is a door
            {
                // Start moving the door upwards
                StartCoroutine(RaiseDoor(hit.collider.gameObject));
            }

            else if (hit.collider.CompareTag("Key") || hit.collider.CompareTag("NoKey"))
            {
               Renderer Ren = hit.collider.GetComponent<Renderer>();
                if (Ren != null)
                {
                    Ren.material.color = Color.green;

                }
            }
            else if (hit.collider.CompareTag("Handle"))
            {
                Open1 = true;
            }
            else if (hit.collider.CompareTag("Handle2"))
            {
                Open2 = true;
            }
            else if (hit.collider.CompareTag("Handle3"))
            {
               Open3 = true;
            }

       else if (Physics.Raycast(ray, out hit, 3))
            {
               if (hit.collider.CompareTag("Letter"))
                  {
                    StartCoroutine(ReadLetter1());
                  } 
            }


        }
    }
    [Header("NightStand")]
    public Transform Opened, Opened2, Opened3;
    public Transform Drawer, Drawer2, Drawer3;
    private bool Open1, Open2, Open3;
    
    public Animator Safe;
    public Transform Hinge;
    private IEnumerator RaiseDoor(GameObject door)
    {
        float raiseAmount = 5f; // The total distance the door will be raised
        float raiseSpeed = 2f; // The speed at which the door will be raised
        Vector3 startPosition = door.transform.position; // Store the initial position of the door
        Vector3 endPosition = startPosition + Vector3.up * raiseAmount; // Calculate the final position of the door after raising
        // Continue raising the door until it reaches the target height
        while (door.transform.position.y < endPosition.y)
        {
            // Move the door towards the target position at the specified speed
            door.transform.position = Vector3.MoveTowards(door.transform.position, endPosition, raiseSpeed * Time.deltaTime);
            yield return null; // Wait until the next frame before continuing the loop
}
    }

    [Header("Letters")]
    public GameObject Letter1;
    public GameObject Letter2;
    public GameObject Letter3;
    public TextMeshProUGUI Speach;
    public GameObject speachBubble;
   IEnumerator ReadLetter1()
    {
        yield return new WaitForSeconds(0);
        Letter1.SetActive(true);
        yield return new WaitForSeconds(10);
        Letter1.SetActive(false);
        yield return new WaitForSeconds(0);
        speachBubble.SetActive(true);
        Speach.text = "Let Me grab a flash light and Look for everyone";
        yield return new WaitForSeconds(3);
        speachBubble.SetActive(false);
        Speach.text = "";
    }
}

