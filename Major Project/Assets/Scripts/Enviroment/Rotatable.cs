using UnityEngine;
using System.Collections;

public class Rotatable : MonoBehaviour {

    public float fRotateSpeed = 6;
    private float fFlipTimer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        fFlipTimer = Mathf.Clamp(fFlipTimer, 0, 1);

        if (Physics.gravity.y >= 0)
        {
            fFlipTimer += fRotateSpeed * Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(new Vector3(0, 0, 0),
                                                 new Vector3(180, 0, 0),
                                                 fFlipTimer);
        }
        else
        {
            fFlipTimer -= fRotateSpeed * Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(new Vector3(0, 0, 0),
                                                  new Vector3(180, 0, 0),
                                                  fFlipTimer);
        }
    }
}
