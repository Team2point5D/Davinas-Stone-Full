using UnityEngine;
using System.Collections;

//Marcus
public class CameraMove : MonoBehaviour
{

    //
    [Header("Distance")]
    public float distanceX;

    public float distanceY;

    public float distanceZ;

    public float speed;

    Camera mainCam;

    Vector3 camLocation;

    public Transform[] cameraLocations;

    PlayerBehaviour playerB;

    Vector3 playerPos;

    // Use this for initialization
    void Start()
    {
        mainCam = Camera.main;

        playerB = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();

        //camLocation = mainCam.transform.localPosition;

        mainCam.transform.parent = null;

        for (int i = 0; i < cameraLocations.Length; i++)
        {
            mainCam.transform.position = cameraLocations[0].transform.position;
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        camLocation = mainCam.transform.position;


        playerPos = GameObject.FindGameObjectWithTag("Player").transform.localPosition;

        playerPos = new Vector3(playerPos.x + distanceX, playerPos.y + distanceY, -distanceZ);

        
        if (playerB.doorExited == true)
        {
            //mainCam.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;

            mainCam.transform.position = Vector3.Lerp(camLocation, playerPos, speed * Time.fixedDeltaTime);

            //mOVE camera

            if (mainCam.transform.position == playerPos)
            {
                mainCam.transform.position = playerPos;
            }


            

        }
        else if (playerB.doorEntered == true)
        {
            mainCam.transform.parent = null;

            int camNum = +1;

            switch (camNum)
            {
                case 1:
                    for (int i = 0; i < cameraLocations.Length; i++)
                    {
                        mainCam.transform.position = cameraLocations[1].transform.position;
                    }
                    break;
                case 2:
                    for (int i = 0; i < cameraLocations.Length; i++)
                    {
                        mainCam.transform.position = cameraLocations[2].transform.position;
                    }
                    break;
                case 3:
                    for (int i = 0; i < cameraLocations.Length; i++)
                    {
                        mainCam.transform.position = cameraLocations[3].transform.position;
                    }
                    break;
                default:
                    print("Error");
                    break;
            }

        }
    }
}
