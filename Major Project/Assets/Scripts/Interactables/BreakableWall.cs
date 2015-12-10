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

            if (col.gameObject.GetComponent<Crate>().bIsObjectHeavy)
            {
               // if ((col.relativeVelocity.y * col.rigidbody.mass >= 100f) ||
               //(col.relativeVelocity.x * col.rigidbody.mass >= 100f) ||
               //(col.relativeVelocity.z * col.rigidbody.mass >= 100f))
               // {
                    BreakWall();
                    Destroy(gameObject);
                //}
            }
		}
	}

}
