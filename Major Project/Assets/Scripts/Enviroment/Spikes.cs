using UnityEngine;
using System.Collections;

//Marcus
public class Spikes : MonoBehaviour {

    //The location of where the player will respawn 
    public Transform respawnPOS; 

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Moves player to respawn position
            col.gameObject.transform.position = respawnPOS.position;
        }

    }

}
