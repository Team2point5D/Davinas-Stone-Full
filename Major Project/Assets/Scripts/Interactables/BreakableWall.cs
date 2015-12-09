using UnityEngine;
using System.Collections;

//David
public class BreakableWall : MonoBehaviour {
	
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Crate")
		{
            Debug.Log(col.relativeVelocity.y * col.rigidbody.mass);
			if(col.relativeVelocity.y * col.rigidbody.mass >= 100f)
			{
				Destroy (gameObject);
			}
		}
	}

}
