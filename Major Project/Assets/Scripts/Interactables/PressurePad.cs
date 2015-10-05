using UnityEngine;
using System.Collections;

//Marcus
public class PressurePad : MonoBehaviour
{
    //Whats the affected object
    public GameObject affectedOBJ;

    public bool isOpen;


    // Use this for initialization
    void Start()
    {
        affectedOBJ.SetActive(true);

    }

    void OnCollisionEnter(Collision col)
    {
        //Changes state depending on the collided objects mass
        if (col.gameObject.tag == "Crate")
        {
            //Heavy
            if (col.gameObject.GetComponent<Rigidbody>().mass >= 10)
            {
                affectedOBJ.SetActive(false);

            }
            //Normal
            else if (col.gameObject.GetComponent<Rigidbody>().mass == 5)
            {
                affectedOBJ.SetActive(true);
            }
            //Light
            //else if (col.gameObject.GetComponent<Rigidbody>().mass <= 1)
            //{

            //}



        }
    }
}
