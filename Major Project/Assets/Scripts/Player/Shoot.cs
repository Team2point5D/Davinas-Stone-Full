using UnityEngine;
using System.Collections;

// Marcus
public class Shoot : MonoBehaviour
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
    private float fShootCooldownReset;
    private bool canShoot;
    private bool bJustShot;
    public bool bJustShotAnim;
    private float fShotAnimTimer = 0.05f;
    public AudioClip acShootSound;
    public float fSonarLifeSpan;

    [Space(10)]

    public Texture2D cursorTex;
    public CursorMode cursMode = CursorMode.Auto;

    AudioSource aSource;

    GameObject player;

    // Use this for initialization
    void Start()
    {
        fShootCooldownReset = fShootCooldown;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        RightStick();

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


        Vector3 rightStickPos = new Vector3(axisX, axisY,10);
        Vector3 worldPosRightStick = Camera.main.ScreenToWorldPoint(rightStickPos);


       // Cursor.SetCursor(cursorTex, worldPosRightStick, cursMode);


    }

    public void ShootMass()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetAxis("RT") == 1) && !bJustShot)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            //print(worldPos);

            GameObject projectile = (GameObject)Instantiate(goBullet, transform.position, Quaternion.identity);

            projectile.transform.LookAt(worldPos);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * fShootSpeed);

            projectile.tag = "Mass Bullet";
            bJustShot = true;
            bJustShotAnim = true;

            gameObject.GetComponent<PlayerBehaviour>().Shooting();
        }
    }

    public void ShootScale()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetAxis("RT") == 1) && !bJustShot)
        {
            Vector3 screenpoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = (Input.mousePosition - screenpoint).normalized;
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
            GameObject projectile = (GameObject)Instantiate(goBullet, tShotSpot.position, rotation);

            projectile.GetComponent<Rigidbody>().velocity = direction * fShootSpeed;
            projectile.tag = "Scale Bullet";
            bJustShot = true;
            bJustShotAnim = true;

            gameObject.GetComponent<PlayerBehaviour>().Shooting();
        }
    }

    public void ShootSonar()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetAxis("RT") == 1) && !bJustShot)
        {
            GameObject projectile = (GameObject)Instantiate(goSonarBullet, transform.position, tShotSpot.rotation);
            Destroy(projectile, fSonarLifeSpan);
            projectile.tag = "Sonar Bullet";
            bJustShot = true;
            bJustShotAnim = true;

            gameObject.GetComponent<PlayerBehaviour>().Shooting();
        }
    }

    public void ThrowObject()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetAxis("RT") == 1) && !bJustShot)
        {
            nearbyCrate.bIsPickedUp = false;
            nearbyCrate.GetComponent<Rigidbody>().isKinematic = false;
            if (gameObject.GetComponent<PlayerBehaviour>().fFlipMove < 0)
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
            else if (gameObject.GetComponent<PlayerBehaviour>().fFlipMove > 0)
            {
                nearbyCrate.GetComponent<Rigidbody>().AddForce(new Vector3(-30f, 30f, 0f), ForceMode.Impulse);
            }
            bHoldingCrate = false;
            bJustShot = true;
            bJustShotAnim = true;
        }
    }
}
