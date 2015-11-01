using UnityEngine;
using System.Collections;

// Marcus
public class FadeInOutMusic : MonoBehaviour
{
    [Header("Songs")]
    public AudioClip song1;

    public AudioClip song2;

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

    // public bool startFade;


    // bool fadeIn;

    // bool fadeOut;

    //float timer;

    AudioSource aSource;



    // Use this for initialization
    void Start()
    {
        aSource = GetComponent<AudioSource>();

        aSource.Play();
        aSource.clip = song1;
        aSource.Play();

    }

    // Update is called once per frame
    void Update()
    {

        if (fadeOut)
        {
            aSource.volume -= Time.deltaTime * fadeTime;

            if (aSource.volume <= minVolume)
            {
                fadeOut = false;

                fadeIn = true;
            }
        }

        if (fadeIn)
        {
            // print("Play");

            aSource.Play();
            aSource.clip = song2;
            aSource.Play();

            aSource.volume += Time.deltaTime * fadeTime;

            if (aSource.volume >= maxVolume)
            {
                fadeIn = false;
            }

        }






    }
}

