using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//David
[RequireComponent(typeof(Rigidbody))]
public class Crate : MonoBehaviour {

    [Header("Interaction")]
    public bool bIsPickedUp = false;

    [Header("Mass")]
	public bool bIsObjectLight = false;
	public bool bIsObjectHeavy = false;
	public bool bIsObjectZeroMass = false;

    [Header("Scale")]
    public bool bIsExpand;
    public bool bChangeSize;

	public float fScaleTimer = 0;
    public float scaleUSize = 1;
    public float scaleSSize = 1;

	private PlayerBehaviour PlayerBehaviour;
    public Transform PlayerHolder;
    private Rigidbody myRigidBody;

	void Start ()
	{
		PlayerBehaviour = GameObject.FindWithTag ("Player").GetComponent<PlayerBehaviour>();
        //PlayerHolder = GameObject.FindWithTag("Holder").GetComponent<Transform>();
        myRigidBody = this.gameObject.GetComponent<Rigidbody>();

        scaleUSize = PlayerBehaviour.scaleUpSize;

        scaleSSize = PlayerBehaviour.scaleDownSize;
	}

	void Update () 
	{
		fScaleTimer = Mathf.Clamp (fScaleTimer, 0, 1);

        if (bIsPickedUp)
        {
            myRigidBody.useGravity = false;
            myRigidBody.isKinematic = true;
            gameObject.transform.position = PlayerHolder.position;
        }
        else
        {
            myRigidBody.useGravity = true;
            myRigidBody.isKinematic = false;
            gameObject.transform.parent = null;
        }

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

	public void ChangeMass ()
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

    public void ChangeScale()
    {

        transform.localScale = Vector3.Lerp (new Vector3(scaleUSize, transform.localScale.y, transform.localScale.z),
                                             new Vector3(scaleSSize, transform.localScale.y, transform.localScale.z),
                                             fScaleTimer);
        if (bIsExpand == true)
        {
            fScaleTimer -= 5 * Time.deltaTime;
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            fScaleTimer += 5 * Time.deltaTime;
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
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
}
