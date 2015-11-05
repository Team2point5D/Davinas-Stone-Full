using UnityEngine;
using System.Collections;

//Marcus
public class RespawnPlayer : MonoBehaviour {

    public Transform respawnArea;


    // Move player to respawn location
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = respawnArea.position;
        }
    }


}
