using UnityEngine;
using System.Collections;

// Created: Marcus
public class MovingPlatform : MonoBehaviour
{
    // Public Variables that set the speed of the platform and the distance in which to change
    [Header("Variables")]
    public float speed;
    public float changeDis;

    // Indicates on whether the platform is going left or right
    [Header("Left Right")]
    public bool leftRight;
    public bool isLeft;

    // Indicates on whether the platform is going up and down
    [Header("Up Down")]
    public bool upDown;
    public bool isUp;

    Vector3 gameOBJPos;

    bool started = true;

    Rigidbody rig;

    // Use this for initialization
    void Start()
    {
        rig = GetComponent<Rigidbody>();

        gameOBJPos = this.gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //changes objects direction Left Right
        if (leftRight == true)
        {
            if (isLeft == true)
            {
                rig.velocity = Vector3.left * Time.fixedDeltaTime * speed;
            }
            else
            {
                rig.velocity = Vector3.right * Time.fixedDeltaTime * speed;
            }
        }
        else if (upDown == true)
        {
            if (isUp == true)
            {
                rig.velocity = Vector3.up * Time.fixedDeltaTime * speed;
            }
            else
            {
                rig.velocity = Vector3.down * Time.fixedDeltaTime * speed;
            }
        }


        //Move direction  Left Right
        if (leftRight == true)
        {
            if (this.gameObject.transform.position.x >= changeDis)
            {
                // print("CHANGE");

                isLeft = !isLeft;

                //The object has moved from its start position
                started = false;
            }

            if (started == false)
            {
                if (this.gameObject.transform.position.x <= gameOBJPos.x)
                {
                    //print("Start Pos");

                    isLeft = false;
                }
            }
        }

        //Up Down
        if (upDown == true)
        {
            if (this.gameObject.transform.localPosition.y >= changeDis)
            {
                isUp = false;

                started = false;
            }

            if (started == false)
            {
                if (this.gameObject.transform.localPosition.y <= gameOBJPos.y)
                {
                    isUp = true;
                }

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
