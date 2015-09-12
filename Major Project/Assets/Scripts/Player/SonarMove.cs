using UnityEngine;
using System.Collections;

public class SonarMove : MonoBehaviour
{
    [Header("Values")]
    public float speed;

    public float lifeTime;

    float timer;

    public bool colourChanged;

    public float colourChangeTime;

    float colourTimer;

    [Header("Audio")]
    public AudioClip sonarSFX;

    public AudioSource AS;

    [Header("Light")]
    public float lightRange;
    public float lightIntensity;
    //public Light sonarLight;

    Rigidbody rig;

    Renderer objRend;


    // Use this for initialization
    void Start()
    {
        AS = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioSource>();

        rig = GetComponent<Rigidbody>();

        AS.clip = sonarSFX;

        AS.Play();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(speed, 0, 0);

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
                print("Change!");

                colourChanged = false;

                colourTimer = 0;

            }
        }

    }

    void OnCollisionEnter(Collision col)
    {
        print("Col");

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
           // print("Colour Change!");

            col.gameObject.GetComponent<Renderer>().material = objRend.material; 
        }

      //  print("Hit");

        Destroy(this.gameObject);
    }
}
