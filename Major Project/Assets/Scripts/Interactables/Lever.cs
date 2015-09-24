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
        if (isOpen)
        {
            affectedDoor.SetActive(false);
        }
        else
        {
            affectedDoor.SetActive(true);
        }
	
	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                print("Press");

                isOpen = !isOpen;
               
            }

        }

    }
}
