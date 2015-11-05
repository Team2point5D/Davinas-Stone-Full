using UnityEngine;
using System.Collections;

public class NewSonarMove : MonoBehaviour {

    //Public values that change the speed of the sonar
    [Header("Values")]
    public float speed;
    public float lifeTime;
    public bool colourChanged;
    public float colourChangeTime;

    float timer;

    float colourTimer;

    // Variables for the point light added to an object that the sonar hits
    [Header("Light")]
    public float lightRange;
    public float lightIntensity;
    //public Light sonarLight;

    Rigidbody rig;

    Renderer objRend;


    // Use this for initialization
    void Start()
    {

        rig = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Adds velocity to the sonar object based on speed
        rig.velocity = new Vector2(speed, 0);

        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            Destroy(this.gameObject);

            timer = 0;
        }

        if (colourChanged == true)
        {
            colourTimer += Time.deltaTime;

            if (colourTimer >= colourChangeTime)
            {
                colourChanged = false;

                colourTimer = 0;
            }
        }

    }

    // Change colour of object and add point light that the sonar hits 
    void OnCollisionEnter(Collision col)
    {
        colourChanged = true;

        objRend = col.gameObject.GetComponent<Renderer>();

        if (colourChanged == true)
        {
            col.gameObject.GetComponent<Renderer>().material.color = Color.red;

            Light sonarLight = col.gameObject.AddComponent<Light>();

            sonarLight.type = LightType.Point;

            sonarLight.range = lightRange;

            sonarLight.intensity = lightIntensity;

            sonarLight.color = Color.red;

        }
        else
        {
            col.gameObject.GetComponent<Renderer>().material = objRend.material;
        }

        Destroy(this.gameObject);
    }
}