using UnityEngine;
using System.Collections;

// Marcus
public class CutSceneDialogue : MonoBehaviour
{

    public AudioClip[] dialogueLines;

    [Space(10)]

    [Range(0, 2)]
    public float waitTime;

    [Space(5)]

    public bool sceneCompleted;

    public bool startScene;

    float timer;

    AudioSource aSource;

    // Use this for initialization
    void Start()
    {
        aSource = GetComponent<AudioSource>();





    }

    // Update is called once per frame
    void Update()
    {

        // print(timer.ToString());

        if (startScene == true)
        {

            //print(i);

          

            aSource.PlayOneShot(dialogueLines[0]);

            startScene = false;

            if (!aSource.isPlaying)
            {
                timer += Time.deltaTime;
            }


            //if (timer >= waitTime)
            //{
            //    aSource.clip = dialogueLines[1];

            //    aSource.Play();

            //    startScene = false;

            //    sceneCompleted = true;


            //}


        }


    }

    void OnTriggerEnter(Collider col)
    {
        if (!sceneCompleted)
        {
            if (col.gameObject.tag == "Player")
            {
                startScene = true;

            }
        }
    }



}
