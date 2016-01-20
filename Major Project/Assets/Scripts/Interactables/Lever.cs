using UnityEngine;
using System.Collections;

//Marcus
public class Lever : MonoBehaviour
{
    //The gameobject that the lever efects
    public GameObject affectedDoor;

    public bool isOpen;


    public GameObject leverOBJ;

    Quaternion leverRot;

    // Use this for initialization
    void Start()
    {
        // Sets the affected door to start 
        affectedDoor.SetActive(true);

      //  leverOBJ = GameObject.Find("Lever");

        leverRot = leverOBJ.transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        //If the door is open and the lever is switched turn the door to be inactive
        if (isOpen)
        {
            affectedDoor.SetActive(false);

            // To fix

            leverOBJ.transform.rotation = new Quaternion(0, 0.1f, 0, 0);

            //leverOBJ.transform.position = new Vector3(transform.position.x + 1, 0, 0);
        }
        else
        {
            affectedDoor.SetActive(true);

            leverOBJ.transform.rotation = leverRot;

        }

    }

    // If the player is in the lever trigger box
    void OnTriggerStay(Collider col)
    {
        // If the player presses Q while in the lever trigger box change open state
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //print("Press");

                isOpen = !isOpen;

            }

        }

    }
}
