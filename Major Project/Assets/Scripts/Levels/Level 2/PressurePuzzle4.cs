using UnityEngine;
using System.Collections;

public class PressurePuzzle4 : MonoBehaviour
{
    public GameObject movingPlatform;
    public float speed;
    public float changeTime;

    float timer;


    public bool canMove;
    public bool moveRight;
    public bool moveLeft;

    Vector3 startPos;

    void Start()
    {
        startPos = movingPlatform.transform.position;
    }

    void Update()
    {
        if (canMove == true)
        {
            timer += Time.deltaTime;

            if (timer >= changeTime)
            {
                moveLeft = false;
                moveRight = true;

                if (timer >= changeTime + changeTime)
                {
                    moveLeft = true;
                    moveRight = false;

                    timer = 0;
                }
            }
 
            if (movingPlatform.transform.position == startPos)
            {
                moveRight = false;
                moveLeft = true;

               // print("Start");

                timer = 0;
            }
        }

        if(moveLeft)
        {
            movingPlatform.GetComponent<Rigidbody>().velocity = Vector3.left * Time.fixedDeltaTime * speed;

            //print("Left");
        }
        else if (moveRight)
        {
            movingPlatform.GetComponent<Rigidbody>().velocity = Vector3.right * Time.fixedDeltaTime * speed;

           // print("Right");
        }


        //print(timer.ToString());
       

    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Crate")
        {
            canMove = true;

            moveLeft = true;
        }
        else
        {
            canMove = false;

            speed = 0;
        }

    }

}
