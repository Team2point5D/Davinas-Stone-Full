using UnityEngine;
using System.Collections;

//Marcus
public class Companion : MonoBehaviour {

    // Light intensity that can be changed by public variables
    [Header("Light")]
    public float minIntensity;
    public float maxIntensity;
    
    //The time of pulsation from little to intense
    [Range(0f,2.5f)]
    public float pulsateTime;


    float multiplier = 2.0f;

    float random;

    Light comPointLight;

	// Use this for initialization
	void Start () 
    {
        comPointLight = GetComponent<Light>();

        // Random number to 
        random = Random.Range(0.0f, 65535.0f);
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        // Generates 'wave' of ransom floats that change based on the number of the random number
        float noise = Mathf.PerlinNoise(random, Time.time * pulsateTime);


        // Lerps the intensity of the light 
        comPointLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);


	}

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
