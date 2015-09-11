using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

    public Transform respawnPOS; 

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {

	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = respawnPOS.position;
            
        }

        if (col.gameObject.tag == "Companion")
        {
            //print("HIT");

            col.gameObject.transform.position = respawnPOS.position;
        }
    }

}
