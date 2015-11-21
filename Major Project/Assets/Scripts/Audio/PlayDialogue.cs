using UnityEngine;
using System.Collections;


// Marcus
public class PlayDialogue : MonoBehaviour {

    public AudioClip Dialogue;

    AudioSource aSource;

	// Use this for initialization
	void Start () 
    {
        aSource = GetComponent<AudioSource>();

      
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            aSource.clip = Dialogue;

            aSource.Play();

        }

    }
}
