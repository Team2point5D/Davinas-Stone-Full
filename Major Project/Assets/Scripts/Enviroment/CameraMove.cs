﻿using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class CameraMove : MonoBehaviour
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


    int camNum;

    // The camera location
    Vector3 camLocation;



    Vector3 playerPos;

    Camera mainCam;


    void Start()
    {
        // Sets maincam to be the main camera
        mainCam = Camera.main;

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

        playerPos = player.gameObject.transform.position;

        playerPos = new Vector3(playerPos.x + distanceX, playerPos.y + distanceY, -distanceZ);


        // Get the player behaviour and check a bool within
        // If that bool is true than move the camera to the player
        if (player.doorExited == true)
        {
            // Lerp the camera to set player position 
            mainCam.transform.position = Vector3.Lerp(camLocation, playerPos, speed * Time.fixedDeltaTime);

            //Move camera 
            if (mainCam.transform.position == playerPos)
            {
                mainCam.transform.position = playerPos;
            }


        }
        else if (player.doorEntered == true)
        {
            // If the player has exited a door
            // Set camera to the next cam location

            //Removes camera as child of player
            mainCam.transform.parent = null;

            // increase an int number by 1
            camNum++;

            player.doorEntered = false;

            //Based on the number of the camNum int, move the camera to the locations in the public array
            switch (camNum)
            {
                case 1:

                    mainCam.transform.position = cameraLocations[1].transform.position;



                    break;
                case 2:

                    mainCam.transform.position = cameraLocations[2].transform.position;



                    break;
                case 3:

                    mainCam.transform.position = cameraLocations[3].transform.position;



                    break;
                case 4:


                    mainCam.transform.position = cameraLocations[4].transform.position;



                    break;
                case 5:

                    mainCam.transform.position = cameraLocations[5].transform.position;

                    break;
                default:
                    print("Error");
                    break;
            }




        }
    }
}
