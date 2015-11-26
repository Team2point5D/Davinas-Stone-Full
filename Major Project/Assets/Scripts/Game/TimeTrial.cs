using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeTrial : MonoBehaviour
{

    public float currentTime;

    public float goalTime;

    float timer;

    [Space(10)]

    public Text timerText;

    public Text timeToBeatText;

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

        string tTBSeconds = Mathf.Floor(goalTime % 60).ToString("00");

        string tTBMinutes = Mathf.Floor(goalTime / 60).ToString("00");


        if (timerRun == true)
        {
            currentTime += Time.deltaTime;

            timerText.text = "Your Time: " + "" + minutes + ":" + seconds;

            timeToBeatText.text = "Time to beat: " +"" + tTBMinutes + ":" + tTBSeconds;

           

            //print("" + minutes + ":" + seconds);

            if (currentTime >= goalTime)
            {
                //timerRun = false;

                print("Done");

            }
        }

    }
}

