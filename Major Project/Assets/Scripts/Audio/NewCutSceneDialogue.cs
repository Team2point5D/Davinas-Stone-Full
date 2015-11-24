using UnityEngine;
using System.Collections;

public class NewCutSceneDialogue : MonoBehaviour {

    public AudioClip[] dialogueLines;

    public bool canPlayCutScene;

    int lineNumber;

    AudioSource aSource;

	// Use this for initialization
	void Start () 
    {
        aSource = GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (canPlayCutScene)
        {
            PlayCutScene();
        }
	
	}

    void PlayCutScene()
    {
        canPlayCutScene = false;

        aSource.clip = dialogueLines[lineNumber];

        aSource.Play();

        StartCoroutine("Wait");

        canPlayCutScene = true;

        lineNumber++;

        if (lineNumber == dialogueLines.Length)
        {
            lineNumber = 0;
        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(aSource.clip.length);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            canPlayCutScene = true;
        }

    }
}
