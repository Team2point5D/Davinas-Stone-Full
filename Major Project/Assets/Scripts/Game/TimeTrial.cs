using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeTrial : MonoBehaviour
{

    public float currentTime;

    public float goalTime;

    float timer;

   // public Text timerText;

    public bool timerRun = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string seconds = Mathf.Floor(currentTime % 60).ToString("00");

        string minutes = Mathf.Floor(currentTime / 60).ToString("00");

        if (timerRun == true)
        {
            currentTime += Time.deltaTime;

           // timerText.text = "" + minutes + ":" + seconds;

            print("" + minutes + ":" + seconds);

            if (currentTime >= goalTime)
            {
                //timerRun = false;

                print("Done");

            }
        }

    }
}

