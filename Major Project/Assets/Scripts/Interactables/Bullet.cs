using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private Transform[] tBulletTrail;
    private GameObject goPlayer;

	void Start ()
    {
        tBulletTrail = gameObject.GetComponentsInChildren<Transform>();
        goPlayer = GameObject.FindWithTag("Player");
	}

	void Update ()
    {
        tBulletTrail[1].LookAt(goPlayer.transform);

        Destroy(gameObject, 5f);
	}
}
