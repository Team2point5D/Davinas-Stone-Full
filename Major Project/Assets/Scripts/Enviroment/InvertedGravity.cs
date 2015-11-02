using UnityEngine;
using System.Collections;

public class InvertedGravity : MonoBehaviour {

    private Rigidbody myRigidBody;

    // Use this for initialization
    void Start()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.gravity.y > 0f)
        {
            myRigidBody.AddForce(new Vector3(0, -10, 0));
        }
        else if (Physics.gravity.y < 0f)
        {
            myRigidBody.AddForce(new Vector3(0, 10, 0));
        }
    }
}
