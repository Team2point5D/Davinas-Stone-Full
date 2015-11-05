using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class CameraMove : MonoBehaviour
{
    // Public variables that can be changed
    [Header("Variables")]
    public float fDampTime = 0.15f;
    private bool bDampDecreasing = false;
    public bool bDampLimit = false;
    private float fDampTimeStarting;
    public float fHorzMargin = 6.3f;
    public float fVertMargin = 6.3f;
    public PlayerBehaviour player;
    public Transform target;
    private Vector3 velocity = Vector3.zero;

    //The set locations of the camera where the puzzles are
    [Header("Camera Locations")]
    private Camera MainCam;
    public float fCameraZoomedIn = 20f;
    public float fCameraZoomedOut = 40f;
    public Transform[] cameraLocations;
    int camNum = 1;

    void Start()
    {
        // Sets maincam to be the main camera
        //mainCam = Camera.main;
        fDampTimeStarting = fDampTime;
        //target = cameraLocations[0].transform;
        MainCam = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        fDampTime = Mathf.Clamp(fDampTime, 0, fDampTimeStarting);
        MainCam.fieldOfView = Mathf.Clamp(MainCam.fieldOfView, fCameraZoomedIn, fCameraZoomedOut);

        if (bDampDecreasing)
        {
            if (!bDampLimit)
            {
                fDampTime -= (1f * fDampTimeStarting) * Time.deltaTime;
                MainCam.fieldOfView -= (1f * (fCameraZoomedOut - fCameraZoomedIn)) * Time.deltaTime;

                if (fDampTime <= 0)
                {
                    bDampLimit = true;
                }
            }
            else
            {
                fDampTime = 0;
                MainCam.fieldOfView = fCameraZoomedIn;
                fHorzMargin = target.position.x + 0.1f;
            }
        }
        else
        {
            if (!bDampLimit)
            {
                MainCam.fieldOfView += (1f * (fCameraZoomedOut - fCameraZoomedIn)) * Time.deltaTime;

                if (MainCam.fieldOfView >= fCameraZoomedOut)
                {
                    bDampLimit = true;
                }
            }
            else
            {
                fDampTime = fDampTimeStarting;
                MainCam.fieldOfView = fCameraZoomedOut;
            }
        }

        if (target != null)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, fDampTime);
        }

        if (this.transform.position.x < fHorzMargin)
        {
            this.transform.position = new Vector3(fHorzMargin, this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.y < fVertMargin)
        {
            this.transform.position = new Vector3(this.transform.position.x, fVertMargin, this.transform.position.z);
        }

        if (player.bDoorExited == true)
        {
            target = player.transform;
            player.bCanUseGravity = false;
            bDampDecreasing = true;
            bDampLimit = false;

            if (player.bIsGravityReversed)
            {
                player.bIsGravityReversed = false;
                player.bPlayerReversed = false;
                Physics.gravity = new Vector3(0, -78.48f, 0);
            }
        }
        else if (player.bDoorEntered == true)
        {
            player.bCanUseGravity = true;
            player.bDoorEntered = false;
            bDampDecreasing = false;
            bDampLimit = false;
            fDampTime = 0.4f;

            //Based on the number of the camNum int, move the camera to the locations in the public array
            camNum++;
            switch (camNum)
            {
                case 1:
                    target = cameraLocations[0].transform;
                    break;
                case 2:
                    target = cameraLocations[1].transform;
                    break;
                case 3:
                    target = cameraLocations[2].transform;
                    break;
                case 4:
                    target = cameraLocations[3].transform;
                    break;
                case 5:
                    target = cameraLocations[4].transform;
                    break;
                case 6:
                    target = cameraLocations[5].transform;
                    break;
                default:
                    print("Error");
                    break;
            }
        }
    }
}
