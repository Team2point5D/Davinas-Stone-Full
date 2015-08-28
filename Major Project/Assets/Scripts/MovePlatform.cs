using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour {

    public float speed;

    public bool isLeft;

    public float changeDis;

   // Vector3 

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (isLeft == true)
        {
            transform.Translate(-speed, 0, 0);
        }
        else
        {
            transform.Translate(speed, 0, 0);
        }
	
	}
}
