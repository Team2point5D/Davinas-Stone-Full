using UnityEngine;
using System.Collections;

public class ShootTest : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetAxis("RT") == 1))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Debug.Log(mousePos);

            GameObject Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Cube.transform.position = mousePos;
        }
	}
}
