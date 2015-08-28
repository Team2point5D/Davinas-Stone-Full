using UnityEngine;
using System.Collections;

public class BreakableWall : MonoBehaviour {
	
	void Start ()
	{

	}

	void Update () 
	{

	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Crate")
		{
			if(col.relativeVelocity.y * col.rigidbody.mass >= 100f)
			{
				Destroy (gameObject);
			}
		}
	}

}
