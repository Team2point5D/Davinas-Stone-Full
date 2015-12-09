using UnityEngine;
using System.Collections;

//Marcus
public class CrumblingPlatform : MonoBehaviour
{

    //public variables that set the times on when platforms will be destroyed and regain
    [Header("Timers")]
    public float fCrumbleTimer;
    public float fRespawnTimer;
    public float fVolume = 1;
    private bool bIsCrumbling;
    private bool bHasFullyCrumbled;
    private float fCrumbleTimerReset;
    private float fRespawnTimerReset;
    private bool bIsShaking;
    private bool bIsTiltedLeft;
    private float fShakeTimer = 0;
    private Vector3 vOriginalRotation;
    private Vector3 vOriginalPosition;
    private MeshRenderer[] platform;
    private MeshCollider[] platformCollider;

    private FMOD_Listener FListener;

    // Use this for initialization
    void Start()
    {
        fCrumbleTimerReset = fCrumbleTimer;
        fRespawnTimerReset = fRespawnTimer;
        platform = GetComponentsInChildren<MeshRenderer>();
        platformCollider = GetComponentsInChildren<MeshCollider>();
        FListener = Camera.main.GetComponent<FMOD_Listener>();
        vOriginalRotation = transform.eulerAngles;
        vOriginalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        fShakeTimer = Mathf.Clamp(fShakeTimer, 0, 1);

        if (bIsShaking)
        {
            if (bIsTiltedLeft)
            {
                fShakeTimer += 50 * Time.deltaTime;

                transform.eulerAngles = Vector3.Lerp(vOriginalRotation,
                                                     vOriginalRotation + new Vector3(0f, 0f, 0.5f),
                                                     fShakeTimer);

                if (fShakeTimer >= 1f)
                {
                    bIsTiltedLeft = false;
                }
            }
            else
            {
                fShakeTimer -= 50 * Time.deltaTime;

                transform.eulerAngles = Vector3.Lerp(vOriginalRotation,
                                                     vOriginalRotation + new Vector3(0f, 0f, -0.5f),
                                                     fShakeTimer);

                if (fShakeTimer <= 0f)
                {
                    bIsTiltedLeft = true;
                }
            }
        }
        else
        {
            transform.eulerAngles = Vector3.Lerp(vOriginalRotation,
                                                 vOriginalRotation,
                                                 1f);
        }

        // Starts the crumbling timer and process
        if (bIsCrumbling && !bHasFullyCrumbled)
        {
            bIsShaking = true;
            fCrumbleTimer -= Time.deltaTime;

            if (fCrumbleTimer <= 0f)
            {
                Crumble();

                for (int i = 0; i < platform.Length; i++)
                {
                    platform[i].enabled = false;
                    platformCollider[i].enabled = false;
                }
                fCrumbleTimer = fCrumbleTimerReset;
                bIsCrumbling = false;
                bHasFullyCrumbled = true;
                bIsShaking = false;
            }
        }

        if (bHasFullyCrumbled)
        {
            fRespawnTimer -= Time.deltaTime;

            if (fRespawnTimer <= 0f)
            {
                for (int i = 0; i < platform.Length; i++)
                {
                    platform[i].enabled = true;
                    platformCollider[i].enabled = true;
                }
                fRespawnTimer = fRespawnTimerReset;
                bHasFullyCrumbled = false;
            }

        }
    }

    void Crumble()
    {
        FMOD_StudioSystem.instance.PlayOneShot("event:/Contact/platformCrumble", transform.position, fVolume);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Parents the player to the platform so they both move
            col.transform.parent = gameObject.transform;
        }
    }

    void OnCollisionStay(Collision col)
    {
        // If the player is on the platform start crumbling
        if (col.gameObject.tag == "Player")
        {
            bIsCrumbling = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        // Stop crumbling 
        if (col.gameObject.tag == "Player")
        {
            //The player is no longer a child of the platform
            col.transform.parent = null;
            bIsCrumbling = false;
            bIsShaking = false;
        }
    }
}
