using UnityEngine;
using System.Collections;

public class NewCutSceneDialogue : MonoBehaviour
{

    public AudioClip[] dialogueLines;

    public bool canPlayCutScene;

    public int lineNumber;

    AudioSource aSource;

    // Use this for initialization
    void Start()
    {
        aSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canPlayCutScene)
        {
            StartCoroutine("PlayCutScene");
        }

    }

    IEnumerator PlayCutScene()
    {
        canPlayCutScene = false;

        aSource.clip = dialogueLines[lineNumber];

        aSource.Play();

        yield return new WaitForSeconds(aSource.clip.length);

        print("Done");

        canPlayCutScene = true;

        lineNumber++;

        // canPlayCutScene = true;

        if (lineNumber == dialogueLines.Length)
        {
            lineNumber = 0;
        }

       // canPlayCutScene = false;

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
