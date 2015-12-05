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
    public float fMoveSpeed;
    public float fJumpHeight;
    public float fJumpIncrease;
    public float fPushPullForce;
    public float fRotateSpeed = 3f;
    private bool bIsGrounded = true;
    private bool bHasJustJumped = false;
    private float fJumpCheckTimer = 0.05f;
    private Animator playerAnimator;
	public string sCurrentGround;
    private float fGroundRayDetectionDistance = 3f;
    private bool bIsFacingRight = true;
    public float fFlipMove;
    private bool bIsMoving;

    [Header("Interaction")]
    private bool bCanClimb;
    private Rigidbody myRigidBody;
    private Companion Companion;
    private bool bNearCrystal;
    private string sCrystalType;
    private GameObject thingToPushPull;
    private GameObject shotParent;

    [Header("Powers")]
    public bool bIsHeavySelected = false;
    public bool bIsGravityReversed = false;
    private bool bHasGravSwitchedOnce = false;
    public float fFlipTimer = 0f;
    public bool bPlayerReversed = false;

    AudioSource aSource;

    [Header("Checks")]
    public bool bCanDoAnything = true;
    public bool bCanUseMagic;
    public bool bCanUseGravity;
    public bool bCanUseMass;
    public bool bCanUseSonar;
    public bool bCanUseScale;
    public float fScaleState = 0;
    public bool bIsMass;
    public bool bIsSonar;
    public bool bIsScale;
    public bool bDoorExited = true;
    public bool bDoorEntered;
    public GameObject goShadow;
    bool pressed;

    private Shoot PlayerShoot;
    Vector3 playerPos;


    void Start()
    {
        //Sets private variables to their corresponding parts via tags and getComponent
        myRigidBody = this.gameObject.GetComponent<Rigidbody>();
        shotParent = GameObject.Find("Magic Shots");
        aSource = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        PlayerShoot = gameObject.GetComponent<Shoot>();
    }

    void Update()
    {
        fFlipTimer = Mathf.Clamp(fFlipTimer, 0, 1);

        if (bCanDoAnything)
        {
            Jump();

            Magic();
        }

        playerAnimator.SetBool("isWalking", bIsMoving);
        playerAnimator.SetBool("hasJumped", bHasJustJumped);
        playerAnimator.SetBool("hasLanded", bIsGrounded);
        playerAnimator.SetBool("hasShot", PlayerShoot.bJustShotAnim);

        if (bHasJustJumped)
        {
            fJumpCheckTimer -= Time.deltaTime;

            if (fJumpCheckTimer <= 0f)
            {
                bHasJustJumped = false;
                fJumpCheckTimer = 0.05f;
            }
        }

        //Flips Player on its x axis when gravity is switched up and down
        if (bPlayerReversed)
        {
            goShadow.SetActive(false);
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
            if (fFlipTimer <= 0f)
            {
                goShadow.SetActive(true);
            }
        }
    }

    void FixedUpdate()
    {
        if (bCanDoAnything)
        {
            Controls();
        }

        // Make a raycast that checks player is on ground or ceilling
        if (bIsGravityReversed == false)
        {
            RaycastHit hitPoint;

            if (Physics.Raycast(transform.position, Vector3.down, out hitPoint, fGroundRayDetectionDistance))
            {
                bIsGrounded = true;
                bHasGravSwitchedOnce = false;
				sCurrentGround = hitPoint.transform.tag;
            }
            else
            {
                bIsGrounded = false;
				sCurrentGround = null;
            }
        }
        else
        {
			RaycastHit hitPoint;
            Debug.Log("Is Checking if grounded reversed");
			if (Physics.Raycast(transform.position, Vector3.down, out hitPoint, fGroundRayDetectionDistance))
            {
                bIsGrounded = true;
                bHasGravSwitchedOnce = false;
				sCurrentGround = hitPoint.transform.tag;
                Debug.Log("Is grounded reversed");
            }
            else
            {
                bIsGrounded = false;
				sCurrentGround = null;
                Debug.Log("Isn't grounded reversed");
            }
        }

        //Flips player left and right
        if (fFlipMove < 0 && !bIsFacingRight)
        {
            Flip();
        }
        else if (fFlipMove > 0 && bIsFacingRight)
        {
            Flip();
        }
    }



    void Controls()
    {
        // Player computer move input
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Vector3 moveQuantity = new Vector3(-fMoveSpeed, 0, 0);
            myRigidBody.velocity = new Vector3(moveQuantity.x, myRigidBody.velocity.y, myRigidBody.velocity.z);
            fFlipMove = 1;
            bIsMoving = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Vector3 moveQuantity = new Vector3(fMoveSpeed, 0, 0);
            myRigidBody.velocity = new Vector3(moveQuantity.x, myRigidBody.velocity.y, myRigidBody.velocity.z);
            fFlipMove = -1;
            bIsMoving = true;
        }

        if (bCanClimb)
        {
            myRigidBody.useGravity = false;

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                Vector3 moveQuantity = new Vector3(0, fMoveSpeed, 0);
                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, moveQuantity.y, myRigidBody.velocity.z);
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                Vector3 moveQuantity = new Vector3(0, -fMoveSpeed, 0);
                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, moveQuantity.y, myRigidBody.velocity.z);
            }

            transform.Translate(Vector3.down * Input.GetAxis("LeftThumbstickY") * fMoveSpeed * Time.deltaTime);
        }
        else
        {
            myRigidBody.useGravity = true;
        }

        // Player xbox controller move input
        transform.Translate(Vector3.right * Input.GetAxis("LeftThumbstickX") * fMoveSpeed * Time.deltaTime);
        if (Input.GetAxis("LeftThumbstickX") > 0)
        {
            fFlipMove = -1;
            bIsMoving = true;
        }
        else if (Input.GetAxis("LeftThumbstickX") < 0)
        {
            fFlipMove = 1;
            bIsMoving = true;
        }
        else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.D))
        {
            bIsMoving = false;
        }

    }

    void Jump()
    {
        //If the player is on the ground or the ceilling
        if (bIsGravityReversed == false)
        {
            if (Input.GetButtonDown("A") && bIsGrounded == true)
            {
                myRigidBody.velocity = (Vector3.up * fJumpHeight);
                bHasJustJumped = true;
            }
        }
        else
        {
            if (Input.GetButtonDown("A") && bIsGrounded == true)
            {
                myRigidBody.velocity = (Vector3.down * fJumpHeight);
                bHasJustJumped = true;
            }
        }
    }

    void Magic()
    {
        if (bNearCrystal == true)
        {
            if (Input.GetButtonDown("X"))
            {
                if(Companion != null)
                {
                    Companion.DestroySelf();
                }
                bCanUseMagic = true;

                if (sCrystalType == "GravityCrystal")
                {
                    bCanUseGravity = true;
                }
                else if (sCrystalType == "MassCrystal")
                {
                    bCanUseMass = true;
                }
                else if (sCrystalType == "SonarCrystal")
                {
                    bCanUseSonar = true;
                }
                else if (sCrystalType == "ScaleCrystal")
                {
                    bCanUseScale = true;
                }
            }
        }

        //Allows player to use shoot mass, sonar and scale magic
        if (bCanUseMagic && bIsMass && !PlayerShoot.bHoldingCrate)
        {
            PlayerShoot.ShootMass();
        }
        else if (bCanUseMagic && bIsSonar && !PlayerShoot.bHoldingCrate)
        {
            PlayerShoot.ShootSonar();
        }
        else if (bCanUseMagic && bIsScale && !PlayerShoot.bHoldingCrate)
        {
            PlayerShoot.ShootScale();
        }
        else if (PlayerShoot.bHoldingCrate)
        {
            PlayerShoot.ThrowObject();
        }

        //Allows use of abilities once crystal is picked up
        if (bCanUseMagic == true)
        {
            // Flip Gravity
            if (bCanUseGravity)
            {
                if (Input.GetButtonDown("Y"))
                {
                    if (bIsGrounded)
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
                    else if (!bIsGrounded && !bHasGravSwitchedOnce)
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
                        bHasGravSwitchedOnce = true;
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
                }
                else if (bIsScale)
                {
                    if (fScaleState == 5)
                    {
                        fScaleState = 0;
                    }
                    else
                    {
                        fScaleState++;
                    }
                }
            }
            else if ((d < 0f) || Input.GetButtonDown("LB"))
            {
                if (bIsMass)
                {
                    bIsHeavySelected = !bIsHeavySelected;
                }
                else if (bIsScale)
                {
                    if (fScaleState == 0)
                    {
                        fScaleState = 5;
                    }
                    else
                    {
                        fScaleState--;
                    }
                }
            }

            //Changing between abilities using the right mouse click
            if (bCanUseMass)
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
        bIsFacingRight = !bIsFacingRight;
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
        if (sCurrentGround == "DirtFloor")
        {
            FMOD_StudioSystem.instance.PlayOneShot("event:/Movement/Walk - run/Run/Dirt run", transform.position, volume);
        }
        else if (sCurrentGround == "GravelFloor")
        {
            FMOD_StudioSystem.instance.PlayOneShot("event:/Movement/Walk - run/Run/Gravel run", transform.position, volume);
        }
        else if (sCurrentGround == "StoneFloor")
        {
            FMOD_StudioSystem.instance.PlayOneShot("event:/Movement/Walk - run/Run/Stone run", transform.position, volume);
        }
        else if (sCurrentGround == "UnevenStoneFloor")
        {
            FMOD_StudioSystem.instance.PlayOneShot("event:/Movement/Walk - run/Run/Uneven stone run", transform.position, volume);
        }
        else if (sCurrentGround == "WoodFloor")
        {
            FMOD_StudioSystem.instance.PlayOneShot("event:/Movement/Walk - run/Run/Wood run", transform.position, volume);
        }
    }

    public void Landing(float volume)
    {
        FMOD_StudioSystem.instance.PlayOneShot("event:/Movement/Jump/Jump landing/Dirt landing", transform.position, volume);
    }

    public void Jumping(float volume)
    {
        FMOD_StudioSystem.instance.PlayOneShot("event:/Movement/Jump/Jump/Jump grunt", transform.position, volume);
    }

    //Collisision and Trigger Events

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Door Exit")
        {
            bDoorExited = true;
            bDoorEntered = false;
        }
        else if (col.gameObject.tag == "Door Enter")
        {
            bDoorEntered = true;
            bDoorExited = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Crate Detection")
        {
            PlayerShoot.nearbyCrate = col.gameObject.GetComponentInParent<Crate>();
        }

        if (col.gameObject.tag == "Magic Area")
        {
            Companion = col.gameObject.GetComponentInParent<Companion>();
            bNearCrystal = true;
            sCrystalType = Companion.gameObject.tag;
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
            bDoorExited = false;
        }
        else if (col.gameObject.tag == "Door Enter")
        {
            bDoorEntered = false;
        }

        if (col.gameObject.tag == "Crate Detection")
        {
            PlayerShoot.nearbyCrate = null;
        }

        if (col.gameObject.tag == "Magic Area")
        {
            Companion = null;
            bNearCrystal = false;
            sCrystalType = null;
        }

        if (col.gameObject.tag == "Climable")
        {
            bCanClimb = false;
        }
    }
}
