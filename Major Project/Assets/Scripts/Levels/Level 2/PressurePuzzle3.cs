using UnityEngine;
using System.Collections;

public class PressurePuzzle3 : MonoBehaviour
{

    [Header("Platform 1")]
    public GameObject platform1;
    public Transform p1HeavyPos;
    Vector3 p1DefaultPos;
    public Transform p1LightPos;

    bool platform1MoveHeavy;
    bool platform1MoveDefault;
    bool platform1MoveLight;


    [Header("Platform 2")]
    public GameObject platform2;
    public Transform p2HeavyPos;
    Vector3 p2DefaultPos;
    public Transform p2LightPos;

    bool platform2MoveHeavy;
    bool platform2MoveDefault;
    bool platform2MoveLight;


    [Header("Platform 3")]
    public GameObject platform3;
    public Transform p3HeavyPos;
    Vector3 p3DefaultPos;
    public Transform p3LightPos;

    bool platform3MoveHeavy;
    bool platform3MoveDefault;
    bool platform3MoveLight;


    public float platformMoveSpeed;


    


    // Use this for initialization
    void Start()
    {
        p1DefaultPos = platform1.transform.position;

        p2DefaultPos = platform2.transform.position;

        p3DefaultPos = platform3.transform.position;
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
            platform1.transform.position = Vector3.Lerp(platform1.transform.position, p1DefaultPos, platformMoveSpeed);

        }
        else if (platform1MoveLight == true)
        {
            platform1.transform.position = Vector3.Lerp(platform1.transform.position, new Vector3(p1LightPos.transform.position.x, p1LightPos.transform.position.y, p1LightPos.transform.position.z), platformMoveSpeed);
        }

        // Platform 2
        if (platform2MoveHeavy == true)
        {
            platform2.transform.position = Vector3.Lerp(platform2.transform.position, new Vector3(p2HeavyPos.transform.position.x, p2HeavyPos.transform.position.y, p2HeavyPos.transform.position.z), platformMoveSpeed);
        }
        else if (platform2MoveDefault == true)
        {
            platform2.transform.position = Vector3.Lerp(platform2.transform.position, p2DefaultPos, platformMoveSpeed);

        }
        else if (platform2MoveLight == true)
        {
            platform2.transform.position = Vector3.Lerp(platform2.transform.position, new Vector3(p2LightPos.transform.position.x, p2LightPos.transform.position.y, p2LightPos.transform.position.z), platformMoveSpeed);
        }

        // Platform3
        if (platform3MoveHeavy == true)
        {
            platform3.transform.position = Vector3.Lerp(platform3.transform.position, new Vector3(p3HeavyPos.transform.position.x, p3HeavyPos.transform.position.y, p3HeavyPos.transform.position.z), platformMoveSpeed);
        }
        else if (platform3MoveDefault == true)
        {
            platform3.transform.position = Vector3.Lerp(platform3.transform.position, p3DefaultPos, platformMoveSpeed);

        }
        else if (platform3MoveLight == true)
        {
            platform3.transform.position = Vector3.Lerp(platform3.transform.position, new Vector3(p3LightPos.transform.position.x, p3LightPos.transform.position.y, p3LightPos.transform.position.z), platformMoveSpeed);
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
                platform2MoveHeavy = true;
                platform3MoveHeavy = true;

                platform1MoveDefault = false;
                platform1MoveLight = false;
                platform2MoveDefault = false;
                platform2MoveLight = false;
                platform3MoveDefault = false;
                platform3MoveLight = false;
                    



            }
            else if (col.gameObject.GetComponent<Rigidbody>().mass == 5)
            {
                //Normal

                platform1MoveDefault = true;
                platform2MoveDefault = true;
                platform3MoveDefault = true;

                platform1MoveHeavy = false;
                platform1MoveLight = false;
                platform2MoveHeavy = false;
                platform2MoveLight = false;
                platform3MoveHeavy = false;
                platform3MoveLight = false;


            }
            else if (col.gameObject.GetComponent<Rigidbody>().mass <= 1)
            {
                //Light

                platform1MoveLight = true;
                platform2MoveLight = true;
                platform3MoveLight = true;

                platform1MoveHeavy = false;
                platform1MoveDefault = false;
                platform2MoveHeavy = false;
                platform2MoveDefault = false;
                platform3MoveHeavy = false;
                platform3MoveDefault = false;

            }



        }
    }
}
