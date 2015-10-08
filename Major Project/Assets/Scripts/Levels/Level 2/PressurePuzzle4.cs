using UnityEngine;
using System.Collections;

public class PressurePuzzle4 : MonoBehaviour
{
    public GameObject movingPlatform;

    public float speed;

    public float changeTime;

    float timer;

    bool canMove;

    Vector3 startPos;

    void Start()
    {
        startPos = movingPlatform.transform.position;
    }

    void FixedUpdate()
    {
        if (canMove == true)
        {
            movingPlatform.GetComponent<Rigidbody>().velocity = Vector3.left * Time.fixedDeltaTime * speed;

            timer += Time.deltaTime;

            if (timer >= changeTime)
            {
                timer = 0;

                canMove = false;

                movingPlatform.GetComponent<Rigidbody>().velocity = Vector3.right * Time.fixedDeltaTime * speed;


            }

            if (movingPlatform.transform.position == startPos)
            {
                canMove = true;

                timer = 0;
            }
        }

       

    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Crate")
        {
            canMove = true;
        }
        else
        {
            canMove = false;

            speed = 0;
        }

    }

}
