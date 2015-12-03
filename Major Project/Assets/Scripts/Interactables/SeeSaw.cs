using UnityEngine;
using System.Collections;

public class SeeSaw : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (transform.gameObject.tag == "Player")
        {
            col.transform.parent = gameObject.transform;
        }

        


        print(col.gameObject.name);
    }
}
