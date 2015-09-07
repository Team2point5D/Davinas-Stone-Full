using UnityEngine;
using System.Collections;

// Created: Marcus
public class MovePlatform : MonoBehaviour
{

    public float speed;

    public bool isLeft;

    public float changeDis;


    Vector3 gameOBJPos;

    bool started = true;

    // Use this for initialization
    void Start()
    {
        gameOBJPos = this.gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //changes objects direction
        if (isLeft == true)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }

        //Move direction
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

    }
}
