using UnityEngine;
using System.Collections;

//Marcus
public class CameraMove : MonoBehaviour
{

    // Public variables that can be changed
    //The distance the camera is from the player character   
    [Header("Distance")]
    public float distanceX;

    public float distanceY;

    public float distanceZ;

    public float speed;

    //The set locations of the camera where the puzzles are
    [Header("Camera Location")]
    public Transform[] cameraLocations;

    public int startingCamLocation;


    // The camera location
    Vector3 camLocation;

    PlayerBehaviour playerB;

    Vector3 playerPos;

    Camera mainCam;


    void Start()
    {
        // Sets maincam to be the main camera
        mainCam = Camera.main;

        // Gets playerbehaviour script
        playerB = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();

        //The main camera does not have a parent as the player starts in a puzzle
        mainCam.transform.parent = null;

        // Sets the first camera in the public array as the starting camera location
        for (int i = 0; i < cameraLocations.Length; i++)
        {
            mainCam.transform.position = cameraLocations[startingCamLocation].transform.position;
        }


    }


    void FixedUpdate()
    {
        camLocation = mainCam.transform.position;

        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        playerPos = new Vector3(playerPos.x + distanceX, playerPos.y + distanceY, -distanceZ);


        // Get the player behaviour and check a bool within
        // If that bool is true than move the camera to the player
        if (playerB.doorExited == true)
        {
            // Lerp the camera to set player position 
            mainCam.transform.position = Vector3.Lerp(camLocation, playerPos, speed * Time.fixedDeltaTime);

            //Move camera 
            if (mainCam.transform.position == playerPos)
            {
                mainCam.transform.position = playerPos;
            }


        }
        else if (playerB.doorEntered == true)
        {
            // If the player has exited a door
            // Set camera to the next cam location

            //Removes camera as child of player
            mainCam.transform.parent = null;

            // increase an int number by 1
            int camNum = +1;

            //Based on the number of the camNum int, move the camera to the locations in the public array
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
