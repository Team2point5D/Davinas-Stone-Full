﻿using UnityEngine;
using System.Collections;

//Marcus
public class CrumblingPlatform : MonoBehaviour
{

    //public variables that set the times on when platforms will be destroyed and regain
    public float timeTillDestroy;

    public float recoveryTime;

    public bool startCrumbling;

    public bool playerIsOn;


    float crumbleTimer;

    float respawnTimer;


    BoxCollider platBoxCol;

    MeshRenderer platMeshRend;


    // Use this for initialization
    void Start()
    { 
        //platBoxCol = GetComponent<BoxCollider>();

        platMeshRend = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Starts the crumbling timer and process
        if (startCrumbling)
        {
            respawnTimer = 0;

            crumbleTimer += Time.fixedDeltaTime;

            if (crumbleTimer >= timeTillDestroy)
            {

               // platBoxCol.enabled = false;

                platMeshRend.enabled = false;

                startCrumbling = false;

            }
        }
        else if (startCrumbling == false)
        {
            

            crumbleTimer = 0;

            respawnTimer += Time.fixedDeltaTime;

            if (respawnTimer >= recoveryTime)
            {
                //platBoxCol.enabled = true;

                platMeshRend.enabled = true;
            }

        }


    }

    void OnCollisionStay(Collision col)
    {
        // If the player is on the platform start crumbling
        if (col.gameObject.tag == "Player")
        {
            startCrumbling = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        // Stop crumbling 
        if (col.gameObject.tag == "Player")
        {
            startCrumbling = false;
        }
    }
}
