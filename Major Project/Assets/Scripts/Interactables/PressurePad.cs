using UnityEngine;
using System.Collections;

//Marcus
public class PressurePad : MonoBehaviour
{

    public GameObject affectedOBJ;

    public bool isOpen;

  

    //Do stuff here

    // Use this for initialization
    void Start()
    {
        affectedOBJ.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
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
