using UnityEngine;
using System.Collections;

public class TestRotate : MonoBehaviour {

	public bool bIsNormal = true;
	public float fRotationTimer = 0;
	
	// Update is called once per frame
	void Update ()
	{
		fRotationTimer = Mathf.Clamp (fRotationTimer, 0, 1);

		if(!bIsNormal)
		{
			fRotationTimer += Time.deltaTime;
			transform.localEulerAngles = Vector3.Lerp (new Vector3(0, 0, 0),
			                                           new Vector3(180, 0, 0),
			                                           fRotationTimer);
		}
		else
		{
			fRotationTimer -= Time.deltaTime;
			transform.localEulerAngles = Vector3.Lerp (new Vector3(0, 0, 0),
			                                           new Vector3(180, 0, 0),
			                                           fRotationTimer);
		}

		if (Input.GetKeyDown(KeyCode.I))
		{
			bIsNormal = !bIsNormal;
		}
	}
}
