using UnityEngine;
using System.Collections;

public class ControllerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButtonDown("Joystick A"))
        {
            print("A");
        }

        if (Input.GetButtonDown("Joystick RB"))
        {
            print("RB");
        }

        if (Input.GetButtonDown("Joystick LB"))
        {
            print("LB");
        }

        if (Input.GetAxis("Joystick RT") >= 1)
        {
            print("RT");
        }

        if (Input.GetAxis("Joystick LT") <= -1)
        {
            print("LT");
        }
	
	}
}
