using UnityEngine;
using System.Collections;

//Marcus
public class Lever : MonoBehaviour
{

    public GameObject affectedDoor;

    public bool isOpen;


    GameObject leverOBJ;

    Quaternion leverRot;

    // Use this for initialization
    void Start()
    {
        affectedDoor.SetActive(true);

        leverOBJ = GameObject.Find("Lever");

        leverRot = leverOBJ.transform.rotation;



    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            affectedDoor.SetActive(false);

            // To fix
            leverOBJ.transform.rotation = new Quaternion(0, 5, 0, 0);  
        }
        else
        {
            affectedDoor.SetActive(true);

            leverOBJ.transform.rotation = leverRot;

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
