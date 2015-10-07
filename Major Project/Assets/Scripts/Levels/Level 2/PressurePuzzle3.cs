using UnityEngine;
using System.Collections;

public class PressurePuzzle3 : MonoBehaviour
{

    [Header("Platform 1")]
    public GameObject platform1;
    public Transform p1HeavyPos;
    Transform p1DefaultPos;
    public Transform p1LightPos;

    bool platform1MoveHeavy;
    bool platform1MoveDefault;
    bool platform1MoveLight;


    [Header("Platform 2")]
    public GameObject platform2;
    [Header("Platform 3")]
    public GameObject platform3;

    public float platformMoveSpeed;


    


    // Use this for initialization
    void Start()
    {
        p1DefaultPos = platform1.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Platform 1
        if (platform1MoveHeavy == true)
        {
            platform1.transform.position = Vector3.Lerp(platform1.transform.position, new Vector3(p1HeavyPos.transform.position.x,p1HeavyPos.transform.position.y,p1HeavyPos.transform.position.z) ,platformMoveSpeed);
        }
        else if(platform1MoveDefault == true)
        {
            platform1.transform.position = Vector3.Lerp(platform1.transform.position, new Vector3(p1DefaultPos.transform.position.x, p1DefaultPos.transform.position.y, p1DefaultPos.transform.position.z), platformMoveSpeed);

        }
        else if (platform1MoveLight == true)
        {
            platform1.transform.position = Vector3.Lerp(platform1.transform.position, new Vector3(p1LightPos.transform.position.x, p1LightPos.transform.position.y, p1LightPos.transform.position.z), platformMoveSpeed);
        }

            


    }

    void OnCollisionStay(Collision col)
    {
        //Changes state depending on the collided objects mass
        if (col.gameObject.tag == "Crate")
        {
            //Heavy
            if (col.gameObject.GetComponent<Rigidbody>().mass >= 10)
            {
                print("HEAVY");

                platform1MoveHeavy = true;



            }
            else if (col.gameObject.GetComponent<Rigidbody>().mass == 5)
            {
                //Normal

                platform1MoveDefault = true;

            }
            else if (col.gameObject.GetComponent<Rigidbody>().mass <= 1)
            {
                //Light

                platform1MoveLight = true;

            }



        }
    }
}
