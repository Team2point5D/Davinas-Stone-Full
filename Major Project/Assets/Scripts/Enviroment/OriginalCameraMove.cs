using UnityEngine;
using System.Collections;

public class OriginalCameraMove : MonoBehaviour
{

    public PlayerBehaviour player;

    // Public variables that can be changed
    //The distance the camera is from the player character 
    [SerializeField]
    [Header("Distance")]
    public float distanceX;

    public float distanceY;

    public float distanceZ;

    public float speed;



    //The set locations of the camera where the puzzles are
    [Header("Camera Location")]
    public Transform[] cameraLocations;

    [Space(10)]
    public int startingCamLocation;

    // The camera location
    Vector3 camLocation;

    int camNum;

    public bool starting;

    Vector3 playerPos;

    Camera mainCam;


    void Start()
    {
        // Sets maincam to be the main camera
        mainCam = Camera.main;

        starting = true;

    }


    void FixedUpdate()
    {


        camLocation = mainCam.transform.position;

        playerPos = player.gameObject.transform.position;

        playerPos = new Vector3(playerPos.x + distanceX, playerPos.y + distanceY, -distanceZ);


        if (starting)
        {
            mainCam.transform.position = playerPos;
        }



       //  Get the player behaviour and check a bool within
      //   If that bool is true than move the camera to the player
            if (player.bDoorExited == true)
            {
                // Lerp the camera to set player position 
                mainCam.transform.position = playerPos;

                mainCam.transform.parent = player.transform;

                //Move camera 
                if (mainCam.transform.position == playerPos)
                {
                    mainCam.transform.position = playerPos;
                }

                // To use gravity
                player.bCanUseGravity = false;

            }
            else if (player.bDoorEntered == true)
            {
                player.bCanUseGravity = true;

                starting = false;

                // If the player has exited a door
                // Set camera to the next cam location

                //Removes camera as child of player
                mainCam.transform.parent = null;

                // increase an int number by 1
                camNum++;

                player.bDoorEntered = false;

                //Based on the number of the camNum int, move the camera to the locations in the public array
                switch (camNum)
                {
                    case 1:
                        mainCam.transform.position = cameraLocations[0].transform.position;
                        break;
                    case 2:
                        mainCam.transform.position = cameraLocations[1].transform.position;
                        break;
                    case 3:
                        mainCam.transform.position = cameraLocations[2].transform.position;
                        break;
                    case 4:
                        mainCam.transform.position = cameraLocations[3].transform.position;
                        break;
                    case 5:
                        mainCam.transform.position = cameraLocations[4].transform.position;
                        break;
                    case 6:
                        mainCam.transform.position = cameraLocations[5].transform.position;
                        break;
                    case 7:
                        mainCam.transform.position = cameraLocations[6].transform.position;
                        break;
                    case 8:
                        mainCam.transform.position = cameraLocations[7].transform.position;
                        break;
                    case 9:
                        mainCam.transform.position = cameraLocations[8].transform.position;
                        break;
                    default:
                        print("Error");
                        break;
                }




            }
    }
}