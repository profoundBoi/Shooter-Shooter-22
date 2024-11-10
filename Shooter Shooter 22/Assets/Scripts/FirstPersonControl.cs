using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
    public Vector2 moveInput; // Stores the movement input from the player
    public Vector2 lookInput; // Stores the look input from the player
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
    public bool holdingGun = false;
    public bool holdingSyth = false;   

    
    [Header("INTERACT SETTINGS")]
    [Space(5)]
    public Material switchMaterial; // Material to apply when switch is activated
    public GameObject[] objectsToChangeColor; // Array of objects to change color

    [Header("Scope Camera")]
    public Camera Cam;
    public bool Scoped = false;
    public int scopeView;

    [Header("Safe Keys")]
    public AudioClip keyPress;
    public AudioClip gunShot;
    [SerializeField]
    AudioSource SFXSRCE;

    [Header("Audio")]
    public AudioClip Error;
    public AudioClip FlashSound;
    public AudioClip SafeOpeningSound;

    [Header("Running")]
    public Slider Stamina;
    public float StaminaSpeed = 1f;


    [Header("Main Character Animation")]
    public Animator mainCA;
    [SerializeField] private bool Walking, RunningWithGun, WalkingWithGun, StandingWithGun, Jumping, Attacking, StandingWithSyth, Running;
    [SerializeField] private bool holdingAGun, holdingASyth;

    [Header("Scrip Reference")]
    public SpeachScript speachScript;
    public void FlashOnAndOff()
    {
        if (holdingFlash == true && !FlashLight.activeSelf)
        {
            FlashLight.SetActive(true);
            SFXSRCE.clip = FlashSound;
            SFXSRCE.Play();
        }
        else if (holdingFlash && FlashLight.activeSelf)
        {
            SFXSRCE.clip = FlashSound;
            FlashLight.SetActive(false);
            SFXSRCE.Play();

        }
    }
    public void Shoot()
    {
        if (holdingGun == true && Ammo > 0 && Weapons[0].tag == "Gun" && speachScript.canLook)
        {
            SFXSRCE.clip = gunShot;
            SFXSRCE.Play();  
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
        else
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, pickUpRange))
            {
            if (hit.collider.CompareTag("Key") || hit.collider.CompareTag("NoKey"))
                {
                    SFXSRCE.clip = keyPress;
                    SFXSRCE.Play();

                    Renderer Ren = hit.collider.GetComponent<Renderer>();
                    if (Ren != null)
                    {
                        Ren.material.color = Color.green;

                    }
                }
            }
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
   
    public void Scope()
    {
        if (holdingGun)
        {
            if (!Scoped)
            {
                Scoped = true;
            }
            else if (Scoped)
            {
                Scoped = false;
            }
        }
        else { return; }
        
    }

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

    
   
    public GameObject Gun;
    private void Awake()
    {

        canLook = true;

        Cursor.visible = false;
        // Get and store the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();
        //gunAim.SetActive(false);
        // pickUpAim.SetActive(true);

        ammoText.text = "";

        //NoKey = GameObject.FindGameObjectsWithTag("NoKey");
        //Key = GameObject.FindGameObjectsWithTag("Key");

        passKey.SetActive(false);
        FlashLight.SetActive(false);

        //Gun.SetActive(false);

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


        // Subscribe to the pick-up input event 
        playerInput.Player.PickUp.performed += ctx => PickUpObject(); //Call the PickUpObject method when pick-up input is performed


        // Subscribe to the interact input event
        playerInput.Player.Interact.performed += ctx => Interact(); // Interact with switch

        // Subscribe to the FlashOn input event
        playerInput.Player.FlashOn.performed += ctx => FlashOnAndOff(); // Turn flash on and off

        playerInput.Player.Scope.performed += ctx => Scope(); // Turn flash on and off

        playerInput.Player.WeaponSwap.performed += ctx => WeaponSwap(); // Turn flash on and off


        playerInput.Player.Sprint.performed += ctx => Sprinted();
        playerInput.Player.Sprint.canceled += ctx => SprintDone();

        playerInput.Player.Pause.performed += ctx => Pause();






    }
    public SceneLoader SL;

    public void Pause()
    {
        SL.PausedButtonClicked();
        Cursor.visible = true;
    }


    public GameObject[] safeCode;
    public GameObject[] unsafeCode;

    public bool CanSprint = true;
    public void Sprinted()
    {
        if (CanSprint)
        {
            if (StaminaSpeed > 0)
            {
                moveSpeed = 15;
                running = true;
                Running = true;
            }

        }
        else { return; }
        
    }

    public void WeaponSwap()
    {
        Weapons.Reverse();
    }

    public void SprintDone()
    {

        running = false;
        moveSpeed = 10;
        Running = false;



    }


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
    private bool running;

    public bool canLook = false;
    private bool ismoving;

    private void Update()
    {
        // Call Move and LookAround methods every frame to handle player movement and camera rotation

        if (speachScript.canMove)
        {
            Move();
        }
        
        ApplyGravity();
        if (speachScript.canLook)
        {
            LookAround();

        }
        
        if (!speachScript.canLook)
        {
            gameObject.tag = "Untagged";
        }
        else if (speachScript.canLook)
        {
            gameObject.tag = "Player";
        }

        #region Weapon swaper
        //if (Weapons.Count > 0)
        //{

        //    heldObject = Weapons[0];

        //}
        //else if (Weapons.Count == 2)
        //{
        //    heldObject = Weapons[0];
        //    Weapons[0].SetActive(true);
        //    Weapons[1].SetActive(false);
        //}
        if (Weapons.Count > 1)
        {
            if (Weapons[0].tag == "Gun")
            {
                Weapons[0].SetActive(true);
                Weapons[1].SetActive(false);
                holdingGun = true;
            }
            else if (Weapons[1].tag == "Gun")
            {
                Weapons[0].SetActive(true);
                holdingAGun = false;
                Gun.SetActive(false);
            }
        }
        #endregion

        #region Animations Without Weapon
        if (characterController.isGrounded )
        {
            Jumping = false;
            if (moveInput.x > 0 || moveInput.y > 0 || moveInput.x < 0 || moveInput.y < 0)
            {
                ismoving = true;
                if (!Running)
                {
                    Walking = true;
                }
                else if (Running && Walking)
                {
                    Walking = false;
                    mainCA.SetBool("Walk", false);
                }
            }
            else
            {
                mainCA.SetBool("Walk", false);
                ismoving = false;

            }


            if (Running && ismoving && characterController.isGrounded)
            {
                mainCA.SetBool("Run", true);
                mainCA.speed = 2;
            }
            else
            {
                mainCA.SetBool("Run", false);
                mainCA.speed = 1;
            }


            if (Walking && ismoving)
            {
                mainCA.SetBool("Walk", true);
            }
            else { mainCA.SetBool("Walk", false); }


        }

        if (characterController.isGrounded != true)
        {
            mainCA.SetBool("Jump", true);
        }
        else { mainCA.SetBool("Jump", false); }

        #endregion

        #region Stamina Stuff

        Stamina.value = StaminaSpeed;

        if (StaminaSpeed > 0)
        {
            CanSprint = true;
        }
        else if (StaminaSpeed <= 0)
        {
            CanSprint = false;
            moveSpeed = 10;
        }
      

        if (StaminaSpeed < 1 && !running)
        {
            StaminaSpeed += 0.002f;
        }
        else if (StaminaSpeed >= 0 && running)
        {
            StaminaSpeed -= 0.005f;
        }
        #endregion

        #region Flash On and OFF
        if (!holdingFlash)
        {
            FlashLight.SetActive(false);
            flashUI.SetActive(false);
        }
        else { flashUI.SetActive(true); }

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
        #endregion

        #region Safe stuff
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
                        SFXSRCE.clip = Error;
                        SFXSRCE.Play();

                    }
                }
            }
        }
        if (OpenSafe())
        {
            Safe.SetBool("Open", true);

            Gun.tag = "Gun";
            
           
            
        }
       
        #endregion

        #region Gun Stuff and Scoping

        if (!holdingGun)
        {
            MeshCollider MC = Gun.GetComponent<MeshCollider>();
            MC.isTrigger = false;
            gunUI.SetActive(false);
        }else
        {
            MeshCollider MC = Gun.GetComponent<MeshCollider>();
            MC.isTrigger= true;

            gunUI.SetActive(true);
        }

        if (Scoped)
        {

            Cam.fieldOfView = scopeView;
        }
        else if (!Scoped)
        {
            Cam.fieldOfView = 60;
        }

        if (!holdingGun)
        {
            Scoped = false;
        }

        
        #endregion

        #region Win Code and animation, and Key Baker

        if (Fang.activeSelf && Eye.activeSelf && Arm.activeSelf && Keys.activeSelf)
        {
            Winner = true;
        }

        if (Winner == true)
        {
           
            Winner = false;
            StartCoroutine(Winners());
        }


        if (Eyed)
        {
            Eye.SetActive(true);
        }

        if (Armed)
        {
            Arm.SetActive(true);
        }

        if (Fanged)
        {
            Fang.SetActive(true);
        }
        if (Unlcoked)
        {
            Keys.SetActive(true);   
        }

        if (Timer == 0 && Baking && !haveKey)
        {
            Key.SetActive(true);
        }
        TimerText.text = "" + Timer;

        if (Irons > 0)
        {
            haveIron = true;
        }
        #endregion






    }

    [Header("Main UI")]
    public GameObject gunUI;
    public GameObject flashUI;
    public GameObject SythUI;
    public GameObject KeyUI;


    #region Win Animation
    IEnumerator Winners()
    {
        Fang.SetActive(false);
        yield return new WaitForSeconds(1);
        Eye.SetActive(false);
        yield return new WaitForSeconds(1);
        Arm.SetActive(false);
        yield return new WaitForSeconds(1);
        ShakeDoorKnoble.SetBool("Shake", true);
        ShakeDoorKnoble.speed = 0.4f;
        yield return new WaitForSeconds(2);
        ShakeDoorKnoble.speed = 0.8f;
        yield return new WaitForSeconds(1.5f);
        ShakeDoorKnoble.speed = 1f;
        yield return new WaitForSeconds(1.5f);
        ShakeDoorKnoble.speed = 1.5f;
        yield return new WaitForSeconds(4);
        WellDone.SetBool("Open", true);
        DoorKnoble.SetActive(false);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("EndGame");
    }
    #endregion 

    public Animator ShakeDoorKnoble;
    public void Move()
    {
        
        // Create a movement vector based on the input
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        // Transform direction from local to world space
        move = transform.TransformDirection(move);

        // Move the character controller based on the movement vector and speed
        characterController.Move(move * moveSpeed * Time.deltaTime);
        
        //Animator Bool
        //Walking = true;
        
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

    public bool holdingFlash = false;
    public void Jump()
    {
        if (characterController.isGrounded )
        {
            // Calculate the jump velocity
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Jumping = true;
        }
    }

    public bool holdingKnife = false;
    public GameObject Knife;
    public Animator anim;

    [Header("Swap Weapon")]
    public List <GameObject> Weapons;

    #region Pickup 
    public void PickUpObject()
    {
        
       


        // Perform a raycast from the camera's position forward
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Debugging: Draw the ray in the Scene view
        Debug.DrawRay(playerCamera.position, playerCamera.forward * pickUpRange, Color.red, 10f);

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {

            // Check if the hit object has the tag "PickUp"
            if (hit.collider.CompareTag("Syth"))
            {


                // Pick up the object
                GameObject HeldSyth = hit.collider.gameObject;
                Weapons.Add(HeldSyth);
                HeldSyth.GetComponent<Rigidbody>().isKinematic = true;// Disable physics


                // Attach the object to the hold position
                HeldSyth.transform.position = sythHoldingPosition.position;
                HeldSyth.transform.rotation = sythHoldingPosition.rotation;
                HeldSyth.transform.parent = sythHoldingPosition;
                holdingSyth = true;
                SythUI.SetActive(true);


            }
            else if (hit.collider.CompareTag("Gun"))
            {

                // Pick up the object
                GameObject HeldGun = hit.collider.gameObject;
                Weapons.Add(HeldGun);
                HeldGun.GetComponent<Rigidbody>().isKinematic = true;// Disable physics

                // Attach the object to the hold position
                HeldGun.transform.position = holdPosition.position;
                HeldGun.transform.eulerAngles = new Vector3(holdPosition.eulerAngles.x, holdPosition.eulerAngles.y, holdPosition.eulerAngles.z);
                HeldGun.transform.parent = holdPosition;
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

            else if (hit.collider.CompareTag("Iron"))
            {
                Irons++;
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("Keys"))
            {
                haveKey = true;
                Destroy(hit.collider.gameObject);
                KeyUI.SetActive(true);

            }


        }
    }
    #endregion

    [Header("Manual Stuff")]
    public GameObject bookManual;
    [SerializeField]
    private bool haveKey, haveIron, Baking;
    [SerializeField]
    private int Irons;
    [SerializeField]
    private int IronsAdded;
    public Animator CloseKeymaker;
    [SerializeField]
    private int Timer;
    public GameObject Key;
    public List<GameObject> ironsInMaker;


    public TextMeshPro TimerText;

    IEnumerator TimerCountDown()
    {
        yield return new WaitForSeconds(1);
        Timer--;
        if (Timer > 0)
        {
            StartCoroutine(TimerCountDown());
        }
        
    }




    IEnumerator FlashLightOn ()
    {
        yield return new WaitForSeconds(0);
        ammoText.text = "RIght Click to Turn on and off";
        yield return new WaitForSeconds(4);
        ammoText.text = "";
    }


    public Vector3 GunRotation;
    private bool holdingBottle = false;

    public int Ammo = 10;
    public GameObject ammoPrefab;
    public GameObject passKey;

    [Header("Emergancy Call")]
    public HealthManager healthManager;

    [Header("Helper UI")]
    public GameObject helperUI;
    public void Interact()
    {
        // Perform a raycast to detect the lightswitch
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            if (hit.collider.CompareTag("EmergancySwitch")) // Assuming the switch has this tag
            {
                healthManager.EmergancyStopped = true;

            }
            else if (hit.collider.CompareTag("Helper")) // Check if the object is a door
            {
                helperUI.SetActive(true);
                Time.timeScale = 0;
                canLook = false;
                Cursor.visible = true;

            }

            else if (hit.collider.CompareTag("DoorK") && haveArm)
            {
                Armed = true;
                haveArm = false;
            }
            else if (hit.collider.CompareTag("DoorK") && haveEye)
            {
                Eyed = true;
                haveEye = false;
            }
            else if (hit.collider.CompareTag("DoorK") && haveFang)
            {
                Fanged = true;
                haveFang = false;
            }
            else if (hit.collider.CompareTag("DoorK") && haveKey)
            {
                Unlcoked = true;
                haveKey = false;
            }
            else if (hit.collider.CompareTag("Manual"))
            {
                bookManual.SetActive (true);
                Cursor.visible = true;
                canLook = false;
            }
            else if (hit.collider.CompareTag("KeyHole") && haveIron )
            {
                IronsAdded++;
                ironsInMaker[0].SetActive (true);
                ironsInMaker.RemoveAt(0);
                
            }
            else if (hit.collider.CompareTag("BakeButton"))
            {
                if (IronsAdded >= 3 && Timer == 30)
                {
                    CloseKeymaker.SetBool("Close", true);
                    StartCoroutine(TimerCountDown());
                    Baking = true;
                    hit.collider.gameObject.tag = null;
                }
            }
            else if (hit.collider.CompareTag("Timer"))
            {
                Timer += 5;
                if (Timer == 60)
                {
                    Timer = 0;  
                }
                
            }






            else if (hit.collider.CompareTag("BDOOR"))
            {
                bool OpenedBDoor = BDOORS.GetBool("OpenBD");

                if (OpenedBDoor)
                {
                    BDOORS.SetBool("OpenBD", false);

                }
                else { BDOORS.SetBool("OpenBD", true); }
            }


             else if (hit.collider.CompareTag("Letter"))
                {
                    StartCoroutine(ReadLetter1());
                }
                if (hit.collider.CompareTag("Letter.3"))
                {
                    StartCoroutine(ReadLetter4());
                }
            


        }
    }
    [Header("NightStand")]
    public Transform Opened, Opened2, Opened3;
    public Transform Drawer, Drawer2, Drawer3;
    private bool Open1, Open2, Open3;
    
    public Animator Safe;
    public Transform Hinge;
    

    [Header("Letters")]
    public GameObject Letter1;
    public GameObject Letter2;
    public GameObject Letter3;
    public GameObject Letter4;
    
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
        canLook = true;
    }

    IEnumerator ReadLetter4()
    {
        yield return new WaitForSeconds(0);
        Letter4.SetActive(true);
        yield return new WaitForSeconds(2);
        Letter4.SetActive(false);
       
    }
    [Header ("Bathroom Animation")]
    public Animator BDOORS;

    public void CloseHelperPanel()
    {
        helperUI.SetActive(false);
        Time.timeScale = 1.0f;
        canLook = true;
        Cursor.visible = false;

    }

    [Header("Door Knoble Suff")]
    public GameObject Eye, Arm, Fang, Keys;
    public GameObject DoorKnoble;
    [SerializeField]
    private bool Eyed, Armed, Fanged, Unlcoked;

    public Animator WellDone;
    [SerializeField]
    private bool Winner;

    [SerializeField]
    private bool haveFang, haveEye, haveArm;

    public GameObject fangUI, eyeUI, armUI;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
         if (hit.collider.gameObject.CompareTag("BunnyArm"))
        {
            haveArm = true;
            hit.collider.isTrigger = true;
            Destroy(hit.gameObject);
            armUI.SetActive(true);

        }
        else if (hit.collider.gameObject.CompareTag("Eye"))
        {
            haveEye = true;
            hit.collider.isTrigger = true;
            Destroy(hit.gameObject);
            eyeUI.SetActive(true);


        }
        else if (hit.collider.gameObject.CompareTag("Fang"))
        {
            haveFang = true;
            hit.collider.isTrigger = true;
            Destroy(hit.gameObject);
            fangUI.SetActive(true);

        }
    }

    
}

