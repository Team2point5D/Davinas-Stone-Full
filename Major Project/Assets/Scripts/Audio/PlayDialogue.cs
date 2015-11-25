using UnityEngine;
using System.Collections;


// Marcus
public class PlayDialogue : MonoBehaviour {

    public AudioClip[] Dialogue;

    [Space(10)]

    public int lineNum;

    public bool canPlay;

    AudioSource aSource;

	// Use this for initialization
	void Start () 
    {
        aSource = GetComponent<AudioSource>();

      
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (canPlay)
        {
            StartCoroutine("PlayDialogueLine");
        }

	
	}

    IEnumerator PlayDialogueLine()
    {
        canPlay = false;

        aSource.clip = Dialogue[lineNum];

        aSource.Play();

        yield return new WaitForSeconds(aSource.clip.length);

        lineNum++;

        if (lineNum == Dialogue.Length)
        {
            lineNum = 0;

            canPlay = false;
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            canPlay = true;
        }

    }
}
