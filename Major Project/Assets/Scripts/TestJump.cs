using UnityEngine;
using System.Collections;

public class TestJump : MonoBehaviour {

    public float jumpHeight;
    public float moveSpeed;
    private Rigidbody myRigidBody;

	// Use this for initialization
	void Start ()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Vector3 moveQuantity = new Vector3(-moveSpeed, 0, 0);
            myRigidBody.velocity = new Vector3(moveQuantity.x, myRigidBody.velocity.y, myRigidBody.velocity.z);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Vector3 moveQuantity = new Vector3 (moveSpeed, 0, 0);
            myRigidBody.velocity = new Vector3(moveQuantity.x, myRigidBody.velocity.y, myRigidBody.velocity.z);
        }

        if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Joystick A")))
        {
            myRigidBody.AddForce(Vector3.up * jumpHeight);
        }
	}
}
