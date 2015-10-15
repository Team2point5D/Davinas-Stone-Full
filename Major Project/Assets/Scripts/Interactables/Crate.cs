using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//David
[RequireComponent(typeof(Rigidbody))]
public class Crate : MonoBehaviour {

    [Header("Mass")]
	public bool bIsObjectLight = false;
	public bool bIsObjectHeavy = false;
	public bool bIsObjectZeroMass = false;

    [Header("Scale")]
    public bool bIsBig;

	public float fScaleTimer = 0;
    public float scaleUSize = 1;
    public float scaleSSize = 1;

	private PlayerBehaviour PlayerBehaviour;

	void Start ()
	{
		PlayerBehaviour = GameObject.FindWithTag ("Player").GetComponent<PlayerBehaviour>();

        scaleUSize = PlayerBehaviour.scaleUpSize;

        scaleSSize = PlayerBehaviour.scaleDownSize;
	}

	void Update () 
	{
		fScaleTimer = Mathf.Clamp (fScaleTimer, 0, 1);

        if (!bIsObjectHeavy && !bIsObjectLight)
        {
            ChangeStateToRegular();
        }

		if(bIsObjectZeroMass && !bIsObjectLight && !bIsObjectHeavy)
		{
            ChangeStateToZeroMass();
		}
        else if (bIsObjectHeavy && !bIsObjectLight)
        {
            ChangeStateToHeavy();
        }
        else if(bIsObjectLight && !bIsObjectHeavy)
        {
            ChangeStateToLight();
        }
	}

	void ChangeMass ()
	{
		if(!bIsObjectZeroMass)
		{
			if(PlayerBehaviour.bIsHeavySelected)
			{
                bIsObjectHeavy = !bIsObjectHeavy;
			}
			else
			{
                bIsObjectLight = !bIsObjectLight;
			}
		}
	}

    void ChangeScale()
    {

        transform.localScale = Vector3.Lerp (new Vector3(scaleUSize, transform.localScale.y, transform.localScale.z),
                                             new Vector3(scaleSSize, transform.localScale.y, transform.localScale.z),
                                             fScaleTimer);
        if (bIsBig == true)
        {
            fScaleTimer -= 5 * Time.deltaTime;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            fScaleTimer += 5 * Time.deltaTime;
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    //Change states based on what is shot at the crate

    void ResetStates()
    {
        bIsObjectHeavy = false;
        bIsObjectLight = false;
        bIsObjectZeroMass = false;
    }

    void ChangeStateToRegular ()
    {
        gameObject.GetComponent<Rigidbody>().mass = 5;
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    void ChangeStateToLight ()
    {
        ResetStates();
        bIsObjectLight = true;
        gameObject.GetComponent<Rigidbody>().mass = 1;
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    void ChangeStateToHeavy ()
    {
        ResetStates();
        bIsObjectHeavy = true;
        gameObject.GetComponent<Rigidbody>().mass = 10;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    void ChangeStateToZeroMass ()
    {
        ResetStates();
        bIsObjectZeroMass = true;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Bullet")
		{
			ChangeMass();
		}

        if (col.gameObject.tag == "Scale Bullet")
        {
            bIsBig = !bIsBig;
        }
	}

}
