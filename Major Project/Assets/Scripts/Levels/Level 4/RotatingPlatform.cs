using UnityEngine;
using System.Collections;

// Marcus
public class RotatingPlatform : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(0, 0, speed);

	
	}
}
