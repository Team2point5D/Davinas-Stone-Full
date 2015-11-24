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

    [Header("Scenes")]
    public bool startScene;
    public bool middleScene;
    public bool endScene;



    [Space(10)]
    [Header("Number of lines")]
    public bool is3Lines;
    public bool is4Lines;
    public bool is5Lines;


    float timer;

    AudioSource aSource;

    // Use this for initialization
    void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (startScene == true)
        {
            if (!aSource.isPlaying)
            {
                timer += Time.deltaTime;

                StartCoroutine("StartDialogue");

                

            }



        }
        else if (startScene == false)
        {
          //  yield return StartCoroutine("Wait");

            //middleScene = true;

            StartCoroutine("MiddleDialogue");
        }

        //print(timer.ToString());

    }



    IEnumerator StartDialogue()
    {
        print("Start");

        yield return StartCoroutine("Wait");

        print("Play 2");

        aSource.clip = dialogueLines[1];

        aSource.Play();

        yield return StartCoroutine("Wait");

        middleScene = true;

        startScene = false;


        //startScene = false;

        //aSource.clip = dialogueLines[2];

        //aSource.Play();

    }

    IEnumerator MiddleDialogue()
    {
        yield return StartCoroutine("Wait");

        aSource.clip = dialogueLines[2];

        aSource.Play();


    }


    IEnumerator Wait()
    {

        yield return new WaitForSeconds(waitTime);

        print("Wait done");
    }

    void OnTriggerEnter(Collider col)
    {
        if (!sceneCompleted)
        {
            if (col.gameObject.tag == "Player")
            {
                startScene = true;

                if (startScene)
                {

                    aSource.clip = dialogueLines[0];

                    aSource.Play();

                    print("Play 1");


                }

            }
        }
    }



}
