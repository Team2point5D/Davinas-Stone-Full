using UnityEngine;
using System.Collections;

//Marcus
public class PressurePad : MonoBehaviour {

    GameObject massOBJ;

    //Do stuff here

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Player")
        {
            //Do stuff here
        }
    }
}
