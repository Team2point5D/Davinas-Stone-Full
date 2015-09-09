using UnityEngine;
using System.Collections;

public class SonarMove : MonoBehaviour
{

    public float speed;

    public float lifeTime;

    float timer;

    public bool colourChanged;

    public float colourChangeTime;

    float colourTimer;

    public AudioClip sonarSFX;

    public AudioSource AS;

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
        }
        else
        {
            print("Colour Change!");

            col.gameObject.GetComponent<Renderer>().material = objRend.material; 
        }

      //  print("Hit");

        Destroy(this.gameObject);
    }
}
