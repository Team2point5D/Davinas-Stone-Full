using UnityEngine;
using System.Collections;

// Created: Marcus
public class MovePlatform : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public float changeDis;

    [Header("Left Right")]
    public bool leftRight;
    public bool isLeft;

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
               rig.velocity = Vector3.left * Time.deltaTime * speed;
            }
            else
            {
                rig.velocity = Vector3.right * Time.deltaTime * speed;
            }
        }
        else if (upDown == true)
        {
            if (isUp == true)
            {
                rig.velocity = Vector3.up * Time.deltaTime * speed;
            }
            else
            {
                rig.velocity = Vector3.down * Time.deltaTime * speed;
            }
        }
        

        //Move direction  Left Right
        if (this.gameObject.transform.localPosition.x >= changeDis)
        {
           // print("CHANGE");

            isLeft = !isLeft;

            started = false;  //The object has moved from its start position
        }

        if (started == false)
        {
            if (this.gameObject.transform.localPosition.x <= gameOBJPos.x)
            {
                //print("Start Pos");

                isLeft = !isLeft;
            }
        }

        //Up Down
        if (this.gameObject.transform.localPosition.y >= changeDis)
        {
            isUp = !isUp;

            started = false;
        }

        if (started == false)
        {
            if (this.gameObject.transform.localPosition.y <= gameOBJPos.y)
            {
                isUp = !isUp;
            }

        }

    }
}
