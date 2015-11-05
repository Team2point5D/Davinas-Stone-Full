using UnityEngine;
using System.Collections;

//Marcus
public class SonarLight : MonoBehaviour
{
    [Header("Light")]
    //public float lightMinIntensity;

    public float lightMaxIntensity;

    [Range(0,2)]
    public float lightSpeed;


    [Header("Range")]
    //public float minRange;
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
        timer += Time.deltaTime;

        //lightSpeed = timer;

        if (startLight)
        {
            lightOBJ.range = Mathf.Lerp(lightOBJ.range, maxRange, rangeSpeed * Time.deltaTime);

            lightOBJ.intensity = Mathf.Lerp(lightOBJ.intensity, lightMaxIntensity, lightSpeed * Time.deltaTime);
        }


    }



}