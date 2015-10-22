using UnityEngine;
using System.Collections;

// Created: Marcus
public class MovingPlatformLeftRight : MonoBehaviour
{
    // Public Variables that set the speed of the platform and the distance in which to change
    [Header("Variables")]
    public float speed;
    public float xChangePosition;

    // Indicates on whether the platform is going left or right
    public bool isLeft;



    float objXPos;


    bool started = true;

    // Use this for initialization
    void Start()
    {
        //gets Rigibody compnent
        //rig = GetComponent<Rigidbody>();

        //rig.useGravity = false;

        //rig.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY| RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        //

        objXPos = gameObject.transform.localPosition.x;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move direction  Left Right
        //Change to make all positive values

        if (this.gameObject.transform.localPosition.x <= xChangePosition)
        {
            //print("CHANGE");

            //isLeft = !isLeft;

            if (isLeft == false)
            {
                isLeft = true;
            }
            else if (isLeft == true)
            {
                isLeft = false;
            }

            //The object has moved from its start position
            started = false;
        }

        if (started == false)
        {
            if (this.gameObject.transform.localPosition.x >= objXPos)
            {
                // print("Start Pos");

                if (isLeft == false)
                {
                    isLeft = true;
                }
                else if (isLeft == true)
                {
                    isLeft = false;
                }

                //isLeft = !isLeft;
            }
        }

        if (isLeft == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(xChangePosition, transform.position.y, transform.position.z), speed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(objXPos, transform.position.y, transform.position.z), speed);
        }





    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Parents the platfrom to the player so they both move
            col.transform.parent = gameObject.transform;
        }



    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {

            //The player so no longer a child of the platform
            col.transform.parent = null;
        }

    }

}
