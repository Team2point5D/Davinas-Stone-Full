using UnityEngine;
using System.Collections;

// Marcus
public class FadeInOutMusic : MonoBehaviour
{

    public AudioClip song1;

    public AudioClip song2;

    [Range(0, 1)]
    public float fadeTime;

    public bool startFade;

    public bool playMusic;

    bool fadeIn;

    bool fadeOut;

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



        if (playMusic)
        {

            if (startFade == true)
            {
                fadeOut = true;
            }

            if (aSource.volume <= 0.15f)
            {
                //print("Fade Up");

                fadeOut = false;

                startFade = false;
            }

            if (fadeOut)
            {
                aSource.volume -= Time.deltaTime * fadeTime;

            }
            else if (!fadeOut)
            {
                print("Play");

                aSource.volume += Time.deltaTime * fadeTime;

                aSource.Play();
                aSource.clip = song2;
                aSource.Play();


                playMusic = false;

            }



        }

        if (!playMusic)
        {
            bool increaseVol = true;

            aSource.volume += Time.deltaTime * fadeTime;

            if (increaseVol)
            {

                if (aSource.volume >= 0.6f)
                {
                    //print("Stop");

                    increaseVol = false;
                }
            }
        }






    }
}
