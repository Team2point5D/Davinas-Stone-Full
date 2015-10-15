using UnityEngine;
using System.Collections;

// Created: Marcus
[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    // Public Variables that set the speed of the platform and the distance in which to change
    [Header("Variables")]
    public float speed;
    public float xChangePosition;
    public float yChangePosition;

    // Indicates on whether the platform is going left or right
    [Header("Left Right")]
    public bool leftRight;
    public bool isLeft;

    // Indicates on whether the platform is going up and down
    [Header("Up Down")]
    public bool upDown;
    public bool isUp;

    float objXPos;
    float objYPos;

    bool started = true;

    Rigidbody rig;

    // Use this for initialization
    void Start()
    {
        //gets Rigibody compnent
        rig = GetComponent<Rigidbody>();

        rig.useGravity = false;

        rig.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        //

        objXPos = gameObject.transform.localPosition.x;

        objYPos = gameObject.transform.localPosition.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move direction  Left Right
        //Change to make all positive values
        if (leftRight == true)
        {
            if (this.gameObject.transform.localPosition.x <= xChangePosition)
            {
                print("CHANGE");

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
                    print("Start Pos");

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
                rig.velocity = Vector3.left * Time.fixedDeltaTime * speed;
            }
            else
            {
                rig.velocity = Vector3.right * Time.fixedDeltaTime * speed;
            }

        }

    


        //Up Down
        if (upDown == true)
        {
            if (this.gameObject.transform.position.y >= yChangePosition)
            {
                if (isUp == false)
                {
                    isUp = true;
                }
                else if (isUp == true)
                {
                    isUp = false;
                }

                started = false;
            }

            if (started == false)
            {
                if (this.gameObject.transform.position.y <= objYPos)
                {
                    if (isUp == false)
                    {
                        isUp = true;
                    }
                    else if (isUp == true)
                    {
                        isUp = false;
                    }
                }

            }

            if (isUp == true)
            {
                rig.velocity = Vector3.up * Time.fixedDeltaTime * speed;
            }
            else
            {
                rig.velocity = Vector3.down * Time.fixedDeltaTime * speed;
            }
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
