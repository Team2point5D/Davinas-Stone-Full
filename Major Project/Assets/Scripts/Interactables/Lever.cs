using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

    public GameObject affectedDoor;

    public bool isOpen;

	// Use this for initialization
	void Start () 
    {
        affectedDoor.SetActive(true);
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                print("Press");

                if (isOpen)
                {
                    affectedDoor.SetActive(false);
                }
               
            }

        }

    }
}
