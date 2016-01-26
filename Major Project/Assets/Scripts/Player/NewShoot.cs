using UnityEngine;
using System.Collections;

// Marcus
public class NewShoot : MonoBehaviour
{

    [Header("Crate")]
    public Crate nearbyCrate;
    public bool bHoldingCrate;

    [Header("Shooting")]
    public float fShootSpeed;
    public Transform tShotSpot;
    public GameObject goBullet;
    public GameObject goSonarBullet;
    public float fShootCooldown = 1.5f;

    public float bulletZPos;

    private float fShootCooldownReset;
    private bool canShoot;
    private bool bJustShot;

    public bool bJustShotAnim;
    private float fShotAnimTimer = 0.05f;

    public AudioClip acShootSound;
    public float fSonarLifeSpan;

    private int iPlayerDirection;
    private int iPlayerReversed;

    [Space(10)]

    //public Texture2D cursorTex;
    //public CursorMode cursMode = CursorMode.Auto;

    public UIHandler UIHandler;

    AudioSource aSource;

    PlayerBehaviour playerBehaviour;

    // Use this for initialization
    void Start()
    {
        fShootCooldownReset = fShootCooldown;

        playerBehaviour = gameObject.GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

        //RightStick();

        if (nearbyCrate)
        {
            if (Input.GetButtonDown("X"))
            {
                nearbyCrate.bIsPickedUp = !nearbyCrate.bIsPickedUp;
                bHoldingCrate = !bHoldingCrate;
            }
        }

        if (bJustShot)
        {
            fShootCooldown -= Time.deltaTime;

            if (fShootCooldown <= 0)
            {
                fShootCooldown = fShootCooldownReset;
                bJustShot = false;
            }
        }

        if (bJustShotAnim)
        {
            fShotAnimTimer -= Time.deltaTime;

            if (fShotAnimTimer <= 0)
            {
                bJustShotAnim = false;
                fShotAnimTimer = 0.05f;
            }
        }

      

        // ShootMass();

      


    }

    void RightStick()
    {

        float axisX = Input.GetAxis("RightStickX");
        float axisY = Input.GetAxis("RightStickY");


        Vector3 rightStickPos = new Vector3(axisX * (1f / 2f) * Screen.width, axisY * (1f / 2f) * Screen.height, 10);
        Vector3 worldPosRightStick = Camera.main.ScreenToWorldPoint(rightStickPos);


    }

    //Shoots a bullet which will alter the mass of a crate to help with puzzle solving.

    public void ShootMass()
    {
        if ((Input.GetMouseButtonDown(0) && !bJustShot))
        {
            print("Shoot");

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = bulletZPos; // Was 10
            Vector3 worldPos;

            if (playerBehaviour.inPuzzle == true)
            {
                print("Im in puzzle");

                worldPos = Camera.main.ViewportToScreenPoint(mousePos);
            }
            else
            {
                worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            }

         
           // Vector3 worldPos = Camera.main.WorldToScreenPoint(mousePos);

         

            GameObject projectile = (GameObject)Instantiate(goBullet, transform.position, transform.rotation);

            projectile.transform.LookAt(worldPos);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * fShootSpeed);

            projectile.tag = "Mass Bullet";
            bJustShot = true;
            bJustShotAnim = true;

            playerBehaviour.Shooting();
        }
    }

    //Shoots a bullet which will alter the size of a crate to help with puzzle solving.

    public void ShootScale()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetAxis("RT") == 1) && !bJustShot)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            GameObject projectile = (GameObject)Instantiate(goBullet, transform.position, Quaternion.identity);

            projectile.transform.LookAt(worldPos);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * fShootSpeed);

            projectile.tag = "Scale Bullet";
            bJustShot = true;
            bJustShotAnim = true;

            playerBehaviour.Shooting();
        }
    }

    //Shoots a light which emits out of the player allowing you to see in levels where it is too dark otherwise.

    public void ShootSonar()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetAxis("RT") == 1) && !bJustShot)
        {
            GameObject projectile = (GameObject)Instantiate(goSonarBullet, transform.position, tShotSpot.rotation);
            Destroy(projectile, fSonarLifeSpan);

            projectile.tag = "Sonar Bullet";
            bJustShot = true;
            bJustShotAnim = true;

            playerBehaviour.Shooting();
        }
    }

    //Throws crate if currently holding one as oppose to shooting.

    public void ThrowObject()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetAxis("RT") == 1) && !bJustShot)
        {
            nearbyCrate.bIsPickedUp = false;
            Rigidbody nearbyCrateRigidbody = nearbyCrate.GetComponent<Rigidbody>();

            nearbyCrateRigidbody.isKinematic = false;

            //Account for which direction the player is looking.

            if (playerBehaviour.fFlipMove < 0)
            {
                iPlayerDirection = 1;
            }
            else
            {
                iPlayerDirection = -1;
            }

            //Accounts for whether player is upside down or on ground.

            if (!playerBehaviour.bIsGravityReversed)
            {
                iPlayerReversed = 1;
            }
            else
            {
                iPlayerReversed = -1;
            }

            //Applies Force in impluse mode to simulate a throw using the above variables to account for different scenarios.

            if (nearbyCrate.bIsObjectHeavy)
            {
                nearbyCrateRigidbody.AddForce(new Vector3(15f * iPlayerDirection, 15f * iPlayerReversed, 0f), ForceMode.Impulse);
            }
            else if (nearbyCrate.bIsObjectLight)
            {
                nearbyCrateRigidbody.AddForce(new Vector3(45f * iPlayerDirection, 40f * iPlayerReversed, 0f), ForceMode.Impulse);
            }
            else
            {
                nearbyCrateRigidbody.AddForce(new Vector3(35f * iPlayerDirection, 55f * iPlayerReversed, 0f), ForceMode.Impulse);
            }
            bHoldingCrate = false;
        }
    }
}