using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
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

        //camLocation = mainCam.transform.localPosition;

        for (int i = 0; i < cameraLocations.Length; i++)
        {
            //print(cameraLocations[i].ToString());

            mainCam.transform.position = cameraLocations[0].transform.position;
        }

        playerB = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        camLocation = mainCam.transform.position;

        playerPos = GameObject.FindGameObjectWithTag("Player").transform.localPosition;

        playerPos = new Vector3(playerPos.x + distanceX, playerPos.y + distanceY, -distanceZ);




        if (playerB.doorCollided == true)
        {
            mainCam.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;

            mainCam.transform.position = Vector3.Lerp(camLocation, playerPos, speed * Time.fixedDeltaTime);
        }
        else
        {
            mainCam.transform.parent = null;
        }


    }
}
