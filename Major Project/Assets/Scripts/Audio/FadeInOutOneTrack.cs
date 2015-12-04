using UnityEngine;
using System.Collections;

// MARCUS
public class FadeInOutOneTrack : MonoBehaviour
{

    //[Header("Song")]
    //public AudioClip song1;

    [Space(5)]

    [Header("Volume")]
    [Range(0, 1)]
    public float maxVolume;

    [Range(0, 1)]
    public float minVolume;

    [Space(5)]

    [Header("Time")]
    [Range(0, 1)]
    public float fadeTime;

    [Space(5)]
    [Header("Checks")]

    public bool fadeOut;

    public bool fadeIn;

    public bool startFade;

    [Space(5)]

    public float waitTime;

    float timer;

    AudioSource aSource;



    // Use this for initialization
    void Start()
    {
        aSource = GetComponent<AudioSource>();

        maxVolume = aSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (!aSource.isPlaying)
        {
            //print("No Play");

            startFade = true;

            if (startFade)
            {
                timer += Time.deltaTime;

                aSource.volume -= Time.deltaTime;

                if (aSource.volume == minVolume)
                {
                    startFade = false;
                }

            }

        }
        else
        {
            timer = 0;
        }

        if (timer >= waitTime)
        {
            aSource.Play();

            startFade = false;
        }

        print(timer.ToString());

    }
}
