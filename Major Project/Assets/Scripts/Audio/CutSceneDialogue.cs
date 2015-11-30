using UnityEngine;
using System.Collections;

// Marcus

[RequireComponent (typeof(AudioSource))]
public class CutSceneDialogue : MonoBehaviour
{

    public AudioClip[] dialogueLines;

    public PlayerBehaviour playerBehave;

    bool canPlayCutScene;

    int lineNumber;

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

            //playerBehave.gameObject.SetActive(false);

            playerBehave.bCanDoAnything = false;
        }

    }

    IEnumerator PlayCutScene()
    {
        canPlayCutScene = false;

        aSource.clip = dialogueLines[lineNumber];

        aSource.Play();

        yield return new WaitForSeconds(aSource.clip.length);

       // print("Done");

        canPlayCutScene = true;

        lineNumber++;

        // canPlayCutScene = true;

        if (lineNumber == dialogueLines.Length)
        {
            lineNumber = 0;

            gameObject.SetActive(false);

            canPlayCutScene = false;

            playerBehave.bCanDoAnything = true;
        }

       // canPlayCutScene = false;

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            canPlayCutScene = true;

            //playerBehave = col.gameObject.GetComponent<PlayerBehaviour>();
        }

    }
}
