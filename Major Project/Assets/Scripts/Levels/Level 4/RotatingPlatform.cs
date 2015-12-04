using UnityEngine;
using System.Collections;

// Marcus
public class RotatingPlatform : MonoBehaviour {

    public float speed;

    [Space(5)]

    public bool rotateOnX;

    public bool rotateOnY;

    public bool rotateOnZ;


	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (rotateOnX)
        {
            transform.Rotate(speed * Time.deltaTime,0,0);
        }

        if (rotateOnY)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }

        if (rotateOnZ)
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }

        //transform.Rotate(Vector3.up, -speed * Time.deltaTime);
	}
}
