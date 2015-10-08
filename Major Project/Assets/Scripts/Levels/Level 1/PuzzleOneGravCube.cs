using UnityEngine;
using System.Collections;

//Marcus
public class PuzzleOneGravCube : MonoBehaviour
{

    public PlayerBehaviour Player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Player.bIsGravityReversed == true)
        {
            // rigidbody.useGravity = true;

            GetComponent<Rigidbody>().AddForce(new Vector3(0, -10, 0));

            // print("Down");
        }
        else if (Player.bIsGravityReversed == false)
        {
            // rigidbody.useGravity = false;

            GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0));

            // print("Up");
        }
    }
}
