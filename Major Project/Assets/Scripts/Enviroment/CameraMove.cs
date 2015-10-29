using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class CameraMove : MonoBehaviour
{
    // Public variables that can be changed
    [Header("Variables")]
    public float fDampTime = 0.15f;
    public float fHorzMargin = 6.3f;
    public float fVertMargin = 6.3f;
    public PlayerBehaviour player;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    private Transform tPlayer;
    public float fZoomIn = 30f;
    private bool bIsZoomedIn;
    private bool bIsZoomedOut;

    //The set locations of the camera where the puzzles are
    [Header("Camera Locations")]
    public Transform[] cameraLocations;
    int camNum = 1;

    void Start()
    {
        // Sets maincam to be the main camera
        //mainCam = Camera.main;
        tPlayer = target;
        target = cameraLocations[0].transform;
        bIsZoomedOut = true;
    }

    void Update()
    {
        if (target != null)
        {
            if (bIsZoomedIn)
            {
                Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
                Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                Vector3 destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(destination.x, destination.y, destination.z + fZoomIn), ref velocity, fDampTime);
                bIsZoomedIn = false;
            }
            else
            {
                Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
                Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                Vector3 destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(destination.x, destination.y, destination.z), ref velocity, fDampTime);
            }


            if (bIsZoomedOut)
            {
                Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
                Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                Vector3 destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(destination.x, destination.y, destination.z - fZoomIn), ref velocity, fDampTime);
                bIsZoomedOut = false;
            }
            else
            {
                Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
                Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                Vector3 destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(destination.x, destination.y, destination.z), ref velocity, fDampTime);
            }
        }

        if (this.transform.position.x < fHorzMargin)
        {
            this.transform.position = new Vector3(fHorzMargin, this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.y < fVertMargin)
        {
            this.transform.position = new Vector3(this.transform.position.x, fVertMargin, this.transform.position.z);
        }

        if (player.doorExited == true)
        {
            target = tPlayer;
            player.bCanUseGravity = false;
            bIsZoomedIn = true;

            if (player.bIsGravityReversed)
            {
                player.bIsGravityReversed = false;
                player.bPlayerReversed = false;
                Physics.gravity = new Vector3(0, -78.48f, 0);
            }

        }
        else if (player.doorEntered == true)
        {
            player.bCanUseGravity = true;
            player.doorEntered = false;
            bIsZoomedOut = true;

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
