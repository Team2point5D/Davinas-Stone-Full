using UnityEngine;
using System.Collections;

//David
public class BreakableWall : MonoBehaviour {

    private float fVolume = 0.8f;

    void BreakWall()
    {
        FMOD_StudioSystem.instance.PlayOneShot("event:/Contact/platformCrumble", transform.position, fVolume);
        FMOD_StudioSystem.instance.PlayOneShot("event:/Contact/rockBreak", transform.position, fVolume);
    }

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Crate")
		{
            Debug.Log(col.relativeVelocity.y * col.rigidbody.mass);
			if(col.relativeVelocity.y * col.rigidbody.mass >= 100f)
			{
                BreakWall();
				Destroy (gameObject);
			}
		}
	}

}
