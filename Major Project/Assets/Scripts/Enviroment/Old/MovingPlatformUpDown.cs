using UnityEngine;
using System.Collections;

//Marcus
public class MovingPlatformUpDown : MonoBehaviour
{

    [Header("Variables")]
    public float speed;
    public float yChangePosition;


    public bool isUp;

    float objYPos;

    bool started = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Up Down

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
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, yChangePosition, transform.position.z), speed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, objYPos, transform.position.z), speed);
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

