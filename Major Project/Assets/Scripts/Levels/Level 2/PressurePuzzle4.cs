using UnityEngine;
using System.Collections;

public class PressurePuzzle4 : MonoBehaviour
{
    public GameObject movingPlatform;

    public float speed;

    public float changeTime;

    float timer;

    bool canMove;

    void FixedUpdate()
    {
        if (canMove == true)
        {
            movingPlatform.GetComponent<Rigidbody>().velocity = Vector3.left * Time.fixedDeltaTime * speed;

            timer += Time.deltaTime;
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
        }

    }

}
