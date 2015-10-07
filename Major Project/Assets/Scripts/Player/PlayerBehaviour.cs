using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;
using System.Collections;

//David
public class PlayerBehaviour : MonoBehaviour
{

    [Header("Powers")]
    public bool bIsHeavySelected = false;
    public bool bIsGravityReversed = false;
    private bool onCompanion;
    private bool inMagic;
    private float fFlipTimer = 0f;
    private bool bPlayerReversed = false;
    //public bool onCrate;

    [Header("Shooting")]
    public float shootSpeed;
    public Transform shotSpot;
    public GameObject shotBullet;
    private bool canShoot;
    public AudioClip shootSound;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpHeight;
    public float jumpIncrease;
    public float pushPullForce;
    private float jumpIncreaseTime;
    private bool bIsGrounded = true;
    public Animator playerAnimator;

    bool isFacingRight = true;
    float flipMove;

    [Range(1f, 100f)]
    [SerializeField]
    float gravityForce;

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

    public bool isUpScale;



    [Header("User Interface")]
    public Texture2D texCursor;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;
    public RectTransform rectCanvas;
    public Image rectAimerFollow;
    public Image imAimer;
    public Text teSelectedAbility;

    public float fClampedY = 0;
    public float fClampedX = 0;


    [Header("Checks")]
    public bool canUseMagic;
    public bool bIsMass;
    public bool bIsSonar;
    public bool bIsScale;
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
        Cursor.SetCursor(texCursor, hotSpot, cursorMode);
    }

    void Update()
    {
        //Sets the aim cursor to mouse's current position on screen
        Vector3 cursorPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position + new Vector3(0f, 0f, 0f));
        rectAimerFollow.rectTransform.anchoredPosition = screenPoint - rectCanvas.sizeDelta / 2f;
        imAimer.rectTransform.position = cursorPosition;
        Cursor.visible = false;

        //Flips Player on its x axis when gravity is switched up and down
        if (bPlayerReversed)
        {
            fFlipTimer += 3 * Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(new Vector3(0, 0, 0),
                                            new Vector3(180, 0, 0),
                                            fFlipTimer);
        }
        else
        {
            fFlipTimer -= 3 * Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(new Vector3(0, 0, 0),
                                                  new Vector3(180, 0, 0),
                                                  fFlipTimer);
        }
    }

    void FixedUpdate()
    {
        //Temporary fix to gravity till we fix issues
        Vector3 extraGravityForce = (Physics.gravity * gravityForce);
        myRigidBody.AddForce(extraGravityForce);

        //Clamps used to prevent values going out of desired bounds
        fFlipTimer = Mathf.Clamp(fFlipTimer, 0, 1);
        float clampedY = Mathf.Clamp(0, 0, 0);
        float clampedZ = Mathf.Clamp(0, 0, 0);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, clampedY, clampedZ);

        //Allows player to use magic once they pick up the crystal
        if (canUseMagic == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                print("Press");

                Vector3 screenpoint = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 direction = (Input.mousePosition - screenpoint).normalized;
                Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
                GameObject projectile = (GameObject)Instantiate(shotBullet, shotSpot.position, rotation);


                projectile.GetComponent<Rigidbody>().velocity = direction * shootSpeed;

                projectile.tag = "Bullet";

                aSource.clip = shootSound;
                aSource.Play();

            }
        }

        // Make a raycast that checks player is on ground or ceilling
        if (bIsGravityReversed == false)
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1.5f))
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
            if (Physics.Raycast(transform.position, Vector3.up, 1.5f))
            {
                bIsGrounded = true;
            }
            else
            {
                bIsGrounded = false;
            }
        }

        Controls();

        Jump();

        Magic();

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
        // Player move input
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

        // Xbox move input
        transform.Translate(Vector3.right * Input.GetAxis("LeftThumbstickX") * moveSpeed * Time.deltaTime);

    }

    void Jump()
    {
        //If the player is on the ground or the ceilling
        if (bIsGravityReversed == false)
        {
            if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Joystick A")) && bIsGrounded == true)
            {
                myRigidBody.velocity = (Vector3.up * jumpHeight);
            }
        }
        else
        {
            if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("A")) && bIsGrounded == true)
            {
                myRigidBody.velocity = (Vector3.down * jumpHeight);
            }
        }
    }

    void Magic()
    {
        if (inMagic == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                if (canUseMagic == false)
                {
                    CompanionnOBJ.SetActive(false);
                    canUseMagic = true;
                }
            }
        }


        //Allows use of abilities once crystal is picked up
        if (canUseMagic == true)
        {
            // Flip Gravity
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (bIsGravityReversed == false)
                {
                    bIsGravityReversed = true;
                    bPlayerReversed = true;
                    Physics.gravity = new Vector3(0, 9.81f, 0);
                }
                else if (bIsGravityReversed == true)
                {
                    bIsGravityReversed = false;
                    bPlayerReversed = false;
                    Physics.gravity = new Vector3(0, -9.81f, 0);
                }
            }

            //Mass change
            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown("2"))
            {
                ChangeStateToMass();
                bIsHeavySelected = !bIsHeavySelected;

				if(bIsHeavySelected)
				{
					Debug.Log ("Mass up");
                    FMOD_StudioSystem.instance.PlayOneShot("event:/Sound effects/Mass up", transform.position);
				}
				else
				{
					Debug.Log ("Mass down");
                    FMOD_StudioSystem.instance.PlayOneShot("event:/Sound effects/Mass down", transform.position);
				}
            }

            //Sonar Shoot
            if (Input.GetKeyDown("3"))
            {
                ChangeStateToSonar();
                GameObject sonarShoot = (GameObject)Instantiate(sonarBull, new Vector3(playerPos.x + sonarDisFromPlayer, playerPos.y + 2, playerPos.z), Quaternion.identity);
            }

            //Shoot Scale
            if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown("4"))
            {
                ChangeStateToScale();
                isUpScale = !isUpScale;
            }
        }
    }

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
        teSelectedAbility.text = "Mass";
    }

    void ChangeStateToScale()
    {
        ResetStates();
        bIsScale = true;
        teSelectedAbility.text = "Scale";
    }

    void ChangeStateToSonar()
    {
        ResetStates();
        bIsSonar = true;
        teSelectedAbility.text = "Sonar";
    }

    //Sounds

    public void Footstep (float volume)
    {
        FMOD_StudioSystem.instance.PlayOneShot("event:/Movement/Walk - run/Run/Grass run", transform.position, volume);
    }

    //Collisisions

    void OnCollisionEnter(Collision col)
    {
        //Picking up crate
        if (col.gameObject.tag == "Crate")
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                col.gameObject.transform.position = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);

                col.gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                col.gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Pushable")
        {
            moveSpeed = 15;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Magic Area")
        {
            CompanionnOBJ = GameObject.FindWithTag("Companion");
            onCompanion = true;
        }
        else if (col.gameObject.tag != "Magic Area")
        {
            CompanionnOBJ = null;
            onCompanion = false;
        }

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
        if (col.gameObject.tag == "Magic Area")
        {
            inMagic = true;
        }

        if (col.gameObject.tag == "Climeable")
        {
            //print("Ladder");

            if (Input.GetKey(KeyCode.Q))
            {
                transform.Translate(0f, 0.5f, 0f);
            }
        }

        if (col.gameObject.tag == "Lever")
        {
            //print("Lever");

            if (Input.GetKeyDown(KeyCode.E))
            {
                // print("Lever Switch");
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Magic Area")
        {
            //print("Im OUT magic");

            inMagic = false;
        }
    }
}
