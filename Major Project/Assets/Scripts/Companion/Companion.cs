using UnityEngine;
using System.Collections;

//Marcus
public class Companion : MonoBehaviour {

    [Header("Light")]
    public float minIntensity;
    public float maxIntensity;
    
    [Range(0f,2.5f)]
    public float pulsateTime;


    float multiplier = 2.0f;

    float random;

    Light comPointLight;

	// Use this for initialization
	void Start () 
    {
        comPointLight = GetComponent<Light>();

        random = Random.Range(0.0f, 65535.0f);
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        float noise = Mathf.PerlinNoise(random, Time.time * pulsateTime);

        comPointLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);

        //comPointLight.intensity = Mathf.Lerp(maxIntensity, minIntensity, pulsateTime);

	
	}
}
