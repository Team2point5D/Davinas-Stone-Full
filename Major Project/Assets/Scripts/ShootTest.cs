using UnityEngine;
using System.Collections;

public class ShootTest : MonoBehaviour {

    public float fShootSpeed;

	// Update is called once per frame
	void Update ()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetAxis("RT") == 1))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            print(mousePos.ToString());

            GameObject Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Cube.AddComponent<Rigidbody>();
            Cube.GetComponent<Rigidbody>().useGravity = false;
            Cube.GetComponent<BoxCollider>().isTrigger = true;
            //Cube.transform.position = worldPos;
            Cube.transform.position = transform.position;
            Cube.transform.LookAt(worldPos);
            Cube.GetComponent<Rigidbody>().AddForce(Cube.transform.forward * fShootSpeed);
        }
	}
}
