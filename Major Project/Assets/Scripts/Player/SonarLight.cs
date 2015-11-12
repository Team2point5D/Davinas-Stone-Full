using UnityEngine;
using System.Collections;

//Marcus
public class SonarLight : MonoBehaviour
{

    public Transform playerOBJ;


    [Header("Light")]
    public float lightMinIntensity;

    public float lightMaxIntensity;

    [Range(0,2)]
    public float lightSpeed;


    [Header("Range")]
    public float minRange;
    public float maxRange;

    [Range(0, 2)]
    public float rangeSpeed;

    [Space(10)]
    public bool startLight;




    float timer;

    Light lightOBJ;

    void Start()
    {
        lightOBJ = GetComponent<Light>();

       
    }

    void Update()
    {
        playerOBJ = transform.Find("Davina");

        transform.parent = playerOBJ;

        timer += Time.deltaTime;

        //lightSpeed = timer;

        if (startLight)
        {
            lightOBJ.range = Mathf.Lerp(lightOBJ.range, maxRange, rangeSpeed * Time.deltaTime);


            lightOBJ.intensity = Mathf.Lerp(lightOBJ.intensity, lightMaxIntensity, lightSpeed * Time.deltaTime);

            if (lightOBJ.intensity >= lightMaxIntensity - 1)
            {
                startLight = false;
            }

        }
        else
        {
            lightOBJ.range = Mathf.Lerp(lightOBJ.range, minRange, rangeSpeed * Time.deltaTime);


            lightOBJ.intensity = Mathf.Lerp(lightOBJ.intensity, lightMinIntensity, lightSpeed * Time.deltaTime);
        }


    }



}