using UnityEngine;
using System.Collections;
//David
public class MovingPlatform : MonoBehaviour {

    [Header("Positions")]
    public float fSpeed;
    private bool bIsMovingToEnd = true;
    public Transform tStart;
    public Transform tEnd;
    private Vector3 vStart;
    private Vector3 vEnd;
    private float fMoveTimer = 0f;
	
    void Start ()
    {
        vStart = tStart.position;
        vEnd = tEnd.position;
        transform.position = vStart;
    }

	void Update () 
    {
        fMoveTimer = Mathf.Clamp(fMoveTimer, 0, 1);

        if (bIsMovingToEnd)
        {
            fMoveTimer += fSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(vStart,
                                              vEnd,
                                              fMoveTimer);

            if (fMoveTimer >= 1f)
            {
                bIsMovingToEnd = false;
            }
        }
        else
        {
            fMoveTimer -= fSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(vStart,
                                              vEnd,
                                              fMoveTimer);
            if (fMoveTimer <= 0f)
            {
                bIsMovingToEnd = true;
            }
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Parents the player to the platform so they both move
            col.transform.parent = gameObject.transform;
        }



    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {

            //The player is no longer a child of the platform
            col.transform.parent = null;
            if (col.gameObject.GetComponent<PlayerBehaviour>().fFlipMove == 1)
            {
                col.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                col.transform.localScale = new Vector3(1, 1, 1);
            }
        }

    }
}
