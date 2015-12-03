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
            mousePos.z = 10;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log(worldPos);
            GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Sphere.AddComponent<Rigidbody>();
            Sphere.GetComponent<Rigidbody>().useGravity = false;
            Sphere.GetComponent<SphereCollider>().isTrigger = true;
            //Sphere.transform.position = worldPos;
            Sphere.transform.LookAt(worldPos);
            Sphere.GetComponent<Rigidbody>().AddForce(Sphere.transform.forward * fShootSpeed);
            Destroy(Sphere, 2f);
        }
	}
}
