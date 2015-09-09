using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public float distance;

    Camera mainCam;

    Vector3 camLocation;

    public Transform[] cameraLocations;

	// Use this for initialization
	void Start () 
    {
        mainCam = Camera.main;

        camLocation = mainCam.transform.localPosition;

        for (int i = 0; i < cameraLocations.Length; i++)
        {
            //print(cameraLocations[i].ToString());

            mainCam.transform.position = cameraLocations[0].transform.position;
        }
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        //mainCam.transform.Translate(Vector3.Lerp(gameObject.transform.localPosition,new Vector3(gameObject.transform.localPosition.x,gameObject.transform.localPosition.y,gameObject.transform.localPosition.z + distance),1));

       

        mainCam.transform.parent = null;

       // mainCam.transform.localPosition = cameraLocations[0].transform.localPosition;



        
        
	}
}
