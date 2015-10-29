using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;
using System.Collections;

//David
public class PlayerBehaviour : MonoBehaviour
{
    [Header("User Interface")]
    public UIHandler UIHandler;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpHeight;
    public float jumpIncrease;
    public float pushPullForce;
    public float fRotateSpeed = 3f;
    private float jumpIncreaseTime;
    public bool bIsGrounded = true;
    public Animator playerAnimator;
    public float fGroundRayDetectionDistance = 1.5f;

    [Header("Interaction")]
    public Crate nearbyCrate;
    private bool bHoldingCrate;
    public bool bCanClimb;

    [Header("Shooting")]
    public float shootSpeed;
    public Transform shotSpot;
    public GameObject shotBullet;
    public GameObject sonarBullet;
    public float fShootCooldown = 1.5f;
    private float fShootCooldownReset;
    private bool canShoot;
    private bool bJustShot;
    public AudioClip shootSound;

    bool isFacingRight = true;
    public float flipMove;

    [Range(1f, 100f)]
    [SerializeField]
    float gravityForce;

    [Header("Powers")]
    public bool bIsHeavySelected = false;
    public bool bIsGravityReversed = false;
    private bool onCompanion;
    private bool inMagic;
    public float fFlipTimer = 0f;
    public bool bPlayerReversed = false;
    //public bool onCrate;

    [Header("Sonar")]
    public GameObject sonarBull;
    public float sonarDisFromPlayer;

    private Rigidbody myRigidBody;

    private GameObject CompanionnOBJ;
    private GameObject thingToPushPull;
    private GameObject shotParent;

    AudioSource aSource;

    [Header("Scale")]
    public float scaleUpSize;
    public float scaleDownSize;

   // public OBJECT_STATE 

    [Header("Checks")]
    public bool bIsUpScale;
    public bool bCanUseMagic;
    public bool bCanUseGravity;
    public bool bCanUseMass;
    public bool bCanUseSonar;
    public bool bCanUseScale;
    bool bIsMass;
    bool bIsSonar;
    bool bIsScale;
    public bool doorExited;
    public bool doorEntered;
    bool pressed;


    Vector3 playerPos;


    void Start()
    {
        //Sets private variables to their corresponding parts via tags and getComponent
        myRigidBody = this.gameObject.GetComponent<Rigidbody>();
        shotParent = GameObject.Find("Magic Shots");
        aSource = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioSource>();
        fShootCooldownReset = fShootCooldown;
    }

    void Update()
    {
        fFlipTimer = Mathf.Clamp(fFlipTimer, 0, 1);

        Jump();

        Magic();

        if (bJustShot)
        {
            fShootCooldown -= Time.deltaTime;

            if (fShootCooldown <= 0)
            {
                fShootCooldown = fShootCooldownReset;
                bJustShot = false;
            }
        }

        if (nearbyCrate)
        {
            if (Input.GetButtonDown("X"))
            {
                nearbyCrate.bIsPickedUp = !nearbyCrate.bIsPickedUp;
                bHoldingCrate = !bHoldingCrate;
            }
        }

        //Flips Player on its x axis when gravity is switched up and down
        if (bPlayerReversed)
        {
            fFlipTimer += fRotateSpeed * Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(new Vector3(0, 0, 0),
                                            new Vector3(180, 0, 0),
                                            fFlipTimer);
        }
        else
        {
            fFlipTimer -= fRotateSpeed * Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(new Vector3(0, 0, 0),
                                                  new Vector3(180, 0, 0),
                                                  fFlipTimer);
        }
    }

    void FixedUpdate()
    {
        Controls();

        //Temporary fix to gravity till we fix issues
        //Vector3 extraGravityForce = (Physics.gravity * gravityForce);
        //myRigidBody.AddForce(extraGravityForce);

        // Make a raycast that checks player is on ground or ceilling
        if (bIsGravityReversed == false)
        {
            if (Physics.Raycast(transform.position, Vector3.down, fGroundRayDetectionDistance))
            {
                bIsGrounded = true;
            }
            else
            {
                bIsGrounded = false;
            }
        }
        else
        {
            if (Physics.Raycast(transform.position, Vector3.up, fGroundRayDetectionDistance))
            {
                bIsGrounded = true;
            }
            else
            {
                bIsGrounded = false;
            }
        }

        //Flips player left and right
        if (flipMove < 0 && !isFacingRight)
        {
            Flip();
        }
        else if (flipMove > 0 && isFacingRight)
        {
            Flip();
        }
    }



    void Controls()
    {
        // Player computer move input
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Vector3 moveQuantity = new Vector3(-moveSpeed, 0, 0);
            myRigidBody.velocity = new Vector3(moveQuantity.x, myRigidBody.velocity.y, myRigidBody.velocity.z);
            flipMove = 1;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Vector3 moveQuantity = new Vector3(moveSpeed, 0, 0);
            myRigidBody.velocity = new Vector3(moveQuantity.x, myRigidBody.velocity.y, myRigidBody.velocity.z);
            flipMove = -1;
        }
        if (bCanClimb)
        {
            myRigidBody.useGravity = false;

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                Vector3 moveQuantity = new Vector3(0, moveSpeed, 0);
                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, moveQuantity.y, myRigidBody.velocity.z);
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                Vector3 moveQuantity = new Vector3(0, -moveSpeed, 0);
                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, moveQuantity.y, myRigidBody.velocity.z);
            }

            transform.Translate(Vector3.down * Input.GetAxis("LeftThumbstickY") * moveSpeed * Time.deltaTime);
        }
        else
        {
            myRigidBody.useGravity = true;
        }

        // Player xbox controller move input
        transform.Translate(Vector3.right * Input.GetAxis("LeftThumbstickX") * moveSpeed * Time.deltaTime);
        if (Input.GetAxis("LeftThumbstickX") > 0)
        {
            flipMove = -1;
        }
        else if (Input.GetAxis("LeftThumbstickX") < 0)
        {
            flipMove = 1;
        }

    }

    void Jump()
    {
        //If the player is on the ground or the ceilling
        if (bIsGravityReversed == false)
        {
            if (Input.GetButtonDown("A") && bIsGrounded == true)
            {
                myRigidBody.velocity = (Vector3.up * jumpHeight);
            }
        }
        else
        {
            if (Input.GetButtonDown("A") && bIsGrounded == true)
            {
                myRigidBody.velocity = (Vector3.down * jumpHeight);
            }
        }
    }

    void Magic()
    {
        if (inMagic == true)
        {
            if (Input.GetButtonDown("X"))
            {
                if (bCanUseMagic == false)
                {
                    CompanionnOBJ.SetActive(false);
                    bCanUseMagic = true;
                    bCanUseGravity = true;
                }
            }
        }

        //Allows player to use shoot mass, sonar and scale magic
        if (bCanUseMagic && bIsMass && !bHoldingCrate)
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetAxis("RT") == 1) && !bJustShot)
            {
                Vector3 screenpoint = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 direction = (Input.mousePosition - screenpoint).normalized;
                Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
                GameObject projectile = (GameObject)Instantiate(shotBullet, shotSpot.position, shotSpot.rotation);

                projectile.GetComponent<Rigidbody>().velocity = direction * shootSpeed;
                projectile.tag = "Mass Bullet";
                bJustShot = true;

                aSource.clip = shootSound;
                aSource.Play();

            }
        }
        else if (bCanUseMagic && bIsSonar && !bHoldingCrate)
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetAxis("RT") == 1) && !bJustShot)
            {
                Vector3 screenpoint = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 direction = (Input.mousePosition - screenpoint).normalized;
                Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
                GameObject projectile = (GameObject)Instantiate(sonarBullet, shotSpot.position, rotation);

                projectile.GetComponent<Rigidbody>().velocity = direction * shootSpeed;
                projectile.tag = "Sonar Bullet";
                bJustShot = true;

                aSource.clip = shootSound;
                aSource.Play();
            }
        }
        else if (bCanUseMagic && bIsScale && !bHoldingCrate)
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetAxis("RT") == 1) && !bJustShot)
            {
                Vector3 screenpoint = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 direction = (Input.mousePosition - screenpoint).normalized;
                Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
                GameObject projectile = (GameObject)Instantiate(shotBullet, shotSpot.position, rotation);

                projectile.GetComponent<Rigidbody>().velocity = direction * shootSpeed;
                projectile.tag = "Scale Bullet";
                bJustShot = true;

                aSource.clip = shootSound;
                aSource.Play();
            }
        }
        else if (bHoldingCrate)
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetAxis("RT") == 1) && !bJustShot)
            {
                nearbyCrate.bIsPickedUp = false;
                nearbyCrate.GetComponent<Rigidbody>().isKinematic = false;
                if (flipMove < 0)
                {
                    if (nearbyCrate.bIsObjectHeavy)
                    {
                        nearbyCrate.GetComponent<Rigidbody>().AddForce(new Vector3(15f, -25f, 0f), ForceMode.Impulse);
                    }
                    else
                    {
                        nearbyCrate.GetComponent<Rigidbody>().AddForce(new Vector3(30f, 30f, 0f), ForceMode.Impulse);
                    }
                }
                else if (flipMove > 0)
                {
                    nearbyCrate.GetComponent<Rigidbody>().AddForce(new Vector3(-30f, 30f, 0f), ForceMode.Impulse);
                }
                bHoldingCrate = false;
                bJustShot = true;
            }
        }

        //Allows use of abilities once crystal is picked up
        if (bCanUseMagic == true)
        {
            // Flip Gravity
            if (bCanUseGravity)
            {
                if (Input.GetButtonDown("Y"))
                {
                    if (bIsGravityReversed == false)
                    {
                        bIsGravityReversed = true;
                        bPlayerReversed = true;
                        Physics.gravity = new Vector3(0, 78.48f, 0);
                    }
                    else if (bIsGravityReversed == true)
                    {
                        bIsGravityReversed = false;
                        bPlayerReversed = false;
                        Physics.gravity = new Vector3(0, -78.48f, 0);
                    }
                }
            }

            //Switching ability modes (mass up to mass down, scaling x up-down to scaling y up-down)
            float d = Input.GetAxis("Mouse ScrollWheel");
            if ((d > 0f) || Input.GetButtonDown("RB"))
            {
                if (bIsMass)
                {
                    bIsHeavySelected = !bIsHeavySelected;
                    UIHandler.SwitchMassUI();
                }
                else if (bIsScale)
                {
                    bIsUpScale = !bIsUpScale;
                }
            }
            else if ((d < 0f) || Input.GetButtonDown("LB"))
            {
                if (bIsMass)
                {
                    bIsHeavySelected = !bIsHeavySelected;
                    UIHandler.SwitchMassUI();
                }
                else if (bIsScale)
                {
                    bIsUpScale = !bIsUpScale;
                }
            }

            //Changing between abilities using the right mouse click
            if (bCanUseMass && !bCanUseSonar)
            {
                ChangeStateToMass();
            }

            if (Input.GetMouseButtonDown(1) || Input.GetButtonDown("B"))
            {
                if (bCanUseMass && bCanUseSonar && !bCanUseScale)
                {
                    if (bIsMass)
                    {
                        ChangeStateToSonar();
                    }
                    else if (bIsSonar)
                    {
                        ChangeStateToMass();
                    }
                }
                else if (bCanUseMass && bCanUseSonar && bCanUseScale)
                {
                    if (bIsMass)
                    {
                        ChangeStateToSonar();
                    }
                    else if (bIsSonar)
                    {
                        ChangeStateToScale();
                    }
                    else if (bIsScale)
                    {
                        ChangeStateToMass();
                    }
                }
            }
        }
    }

    //Function used to flip player to where they are walking

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //States used to switch between abilities

    void ResetStates()
    {
        bIsMass = false;
        bIsScale = false;
        bIsSonar = false;
    }

    void ChangeStateToMass()
    {
        ResetStates();
        bIsMass = true;
        UIHandler.teSelectedAbility.text = "Mass";
    }

    void ChangeStateToScale()
    {
        ResetStates();
        bIsScale = true;
        UIHandler.teSelectedAbility.text = "Scale";
    }

    void ChangeStateToSonar()
    {
        ResetStates();
        bIsSonar = true;
        UIHandler.teSelectedAbility.text = "Sonar";
    }

    //Sounds

    public void Footstep(float volume)
    {
        FMOD_StudioSystem.instance.PlayOneShot("event:/Movement/Walk - run/Run/Dirt run", transform.position, volume);
    }

    //Collisision and Trigger Events

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Door Exit")
        {
            doorExited = true;
            doorEntered = false;
        }
        else if (col.gameObject.tag == "Door Enter")
        {
            doorEntered = true;
            doorExited = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Crate Detection")
        {
            nearbyCrate = col.gameObject.GetComponentInParent<Crate>();
        }

        if (col.gameObject.tag == "Magic Area")
        {
            CompanionnOBJ = GameObject.FindWithTag("Companion");
            inMagic = true;
        }

        if (col.gameObject.tag == "Climable")
        {
            bCanClimb = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Door Exit")
        {
            doorExited = false;
        }
        else if (col.gameObject.tag == "Door Enter")
        {
            doorEntered = false;
        }

        if (col.gameObject.tag == "Crate Detection")
        {
            nearbyCrate = null;
        }

        if (col.gameObject.tag == "Magic Area")
        {
            CompanionnOBJ = null;
            inMagic = false;
        }

        if (col.gameObject.tag == "Climable")
        {
            bCanClimb = false;
        }
    }
}
