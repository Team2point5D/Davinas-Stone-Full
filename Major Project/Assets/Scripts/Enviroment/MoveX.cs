using UnityEngine;
using System.Collections;

public class MoveX : MonoBehaviour {

    [Header("Positions")]
    public float fSpeed;
    public float fEndPosX;
    private bool bIsMovingToEnd;
    private float fStartPosX;
    private float fCurrentPosX;
    private float fMoveTimer = 0f;

	// Use this for initialization
	void Start () 
    {
        fStartPosX = gameObject.transform.localPosition.x;
	}
	
	// Update is called once per frame
	void Update () 
    {
        fMoveTimer = Mathf.Clamp(fMoveTimer, 0, 1);
        fCurrentPosX = gameObject.transform.localPosition.x;

        if (fCurrentPosX == fStartPosX)
        {
            bIsMovingToEnd = true;
        }

        if (bIsMovingToEnd)
        {
            fMoveTimer += fSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(new Vector3(fStartPosX, transform.position.y, transform.position.z),
                                              new Vector3(fEndPosX, transform.position.y, transform.position.z),
                                              fMoveTimer);

            if (fMoveTimer >= 1f)
            {
                bIsMovingToEnd = false;
            }
        }
        else
        {
            fMoveTimer -= fSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(new Vector3(fStartPosX, transform.position.y, transform.position.z),
                                              new Vector3(fEndPosX, transform.position.y, transform.position.z),
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
            //Parents the platfrom to the player so they both move
            col.transform.parent = gameObject.transform;
        }



    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {

            //The player so no longer a child of the platform
            col.transform.parent = null;
        }

    }
}
