﻿using UnityEngine;
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
    private float jumpIncreaseTime;
    public bool bIsGrounded = true;
    public Animator playerAnimator;
    public float fGroundRayDetectionDistance = 1.5f;

    [Header("Interaction")]
    public Crate nearbyCrate;

    [Header("Shooting")]
    public float shootSpeed;
    public Transform shotSpot;
    public GameObject shotBullet;
    private bool canShoot;
    public AudioClip shootSound;

    bool isFacingRight = true;
    float flipMove;

    [Range(1f, 100f)]
    [SerializeField]
    float gravityForce;

    [Header("Powers")]
    public bool bIsHeavySelected = false;
    public bool bIsGravityReversed = false;
    private bool onCompanion;
    private bool inMagic;
    private float fFlipTimer = 0f;
    private bool bPlayerReversed = false;
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
    }

    void Update()
    {
        Jump();

        Magic();

        if (nearbyCrate)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                nearbyCrate.bIsPickedUp = !nearbyCrate.bIsPickedUp;
            }
        }

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
        Controls();

        //Temporary fix to gravity till we fix issues
        Vector3 extraGravityForce = (Physics.gravity * gravityForce);
        myRigidBody.AddForce(extraGravityForce);

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
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                if (bCanUseMagic == false)
                {
                    CompanionnOBJ.SetActive(false);
                    bCanUseMagic = true;
                    bCanUseGravity = true;
                }
            }
        }


        //Allows use of abilities once crystal is picked up
        if (bCanUseMagic == true)
        {
            // Flip Gravity
            if (bCanUseGravity)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl))
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
            }

            //Allows player to use shoot mass, sonar and scale magic
            if (bCanUseMagic == true)
            {
                if (bIsMass)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 screenpoint = Camera.main.WorldToScreenPoint(transform.position);
                        Vector3 direction = (Input.mousePosition - screenpoint).normalized;
                        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
                        GameObject projectile = (GameObject)Instantiate(shotBullet, shotSpot.position, rotation);

                        projectile.GetComponent<Rigidbody>().velocity = direction * shootSpeed;
                        projectile.tag = "Mass Bullet";

                        aSource.clip = shootSound;
                        aSource.Play();

                    }
                }
                else if (bIsSonar)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //GameObject sonarShoot = (GameObject)Instantiate(sonarBull, new Vector3(playerPos.x + sonarDisFromPlayer, playerPos.y + 2, playerPos.z), Quaternion.identity);
                    }
                }
                else if (bIsScale)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 screenpoint = Camera.main.WorldToScreenPoint(transform.position);
                        Vector3 direction = (Input.mousePosition - screenpoint).normalized;
                        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
                        GameObject projectile = (GameObject)Instantiate(shotBullet, shotSpot.position, rotation);

                        projectile.GetComponent<Rigidbody>().velocity = direction * shootSpeed;
                        projectile.tag = "Scale Bullet";

                        aSource.clip = shootSound;
                        aSource.Play();
                    }
                }
            }

            //Switching ability modes (mass up to mass down, scaling x up-down to scaling y up-down)
            float d = Input.GetAxis("Mouse ScrollWheel");
            if (d > 0f)
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
            else if (d < 0f)
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

            if (Input.GetMouseButtonDown(1))
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
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Crate Detection")
        {
            nearbyCrate = null;
        }

        if (col.gameObject.tag == "Magic Area")
        {
            CompanionnOBJ = null;
            inMagic = false;
        }
    }
}
