using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
