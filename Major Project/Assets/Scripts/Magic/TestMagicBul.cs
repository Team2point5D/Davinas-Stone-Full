using UnityEngine;
using System.Collections;

//Marcus
public class TestMagicBul : MonoBehaviour {

    public float lifeTime;

    float timer;

    

	// Use this for initialization
	void Start () 
    {
        
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            Destroy(this.gameObject);

            timer = 0;
        }
	
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Crate" || col.gameObject.tag == "Obstacle" || col.gameObject.tag == "Companion")
		{
			Destroy (gameObject);
		}
	}
}
