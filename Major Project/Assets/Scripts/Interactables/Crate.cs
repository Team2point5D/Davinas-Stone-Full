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
    public bool bIsObjectExpanded = false;
    public bool bIsObjectContracted = false;
    public bool bIsScaleUp = true;
    public bool bChangeSize;

	public float fScaleTimer = 0;
    private bool bIsScaling = false;
    private Vector3 vStartingSize;
    private Vector3 vEndingSize;
    public float fScaleXUpSize = 1;
    public float fScaleDownSize = 1;

	private PlayerBehaviour PlayerBehaviour;
    public Transform PlayerHolder;
    private Rigidbody myRigidBody;

	void Start ()
	{
		PlayerBehaviour = GameObject.FindWithTag ("Player").GetComponent<PlayerBehaviour>();
        //PlayerHolder = GameObject.FindWithTag("Holder").GetComponent<Transform>();
        myRigidBody = this.gameObject.GetComponent<Rigidbody>();

        vStartingSize = gameObject.transform.localScale;
	}

	void Update () 
	{
		fScaleTimer = Mathf.Clamp (fScaleTimer, 0, 1);

        if (bIsScaling)
        {
            transform.localScale = Vector3.Lerp(transform.localScale,
                                                vEndingSize,
                                                fScaleTimer);

            fScaleTimer += Time.deltaTime;

            if (fScaleTimer >= 1f)
            {
                bIsScaling = false;
                fScaleTimer = 0f;
            }
        }

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

        if (!bIsObjectHeavy && !bIsObjectLight && !bIsObjectExpanded && !bIsObjectContracted)
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

        if (bIsObjectContracted && !bIsObjectExpanded)
        {
            ChangeStateToContracted();
        }
        else if (bIsObjectExpanded && !bIsObjectContracted)
        {
            ChangeStateToExpanded();
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
        if (PlayerBehaviour.bIsScalingUp)
        {
            bIsObjectExpanded = !bIsObjectExpanded;
            Debug.Log("Expand");
        }
        else
        {
            bIsObjectContracted = !bIsObjectContracted;
            Debug.Log("Contract");
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
        bIsScaling = true;
        vEndingSize = vStartingSize;
        gameObject.GetComponent<Rigidbody>().mass = 5;
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    void ChangeStateToLight ()
    {
        ResetStates();
        bIsObjectLight = true;
        gameObject.GetComponent<Rigidbody>().mass = 2.5f;
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

    void ChangeStateToExpanded()
    {
        bIsScaling = true;
        vEndingSize = new Vector3(fScaleXUpSize, transform.localScale.y, transform.localScale.z);
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
    void ChangeStateToContracted()
    {
        bIsScaling = true;
        vEndingSize = new Vector3(fScaleDownSize, transform.localScale.y, transform.localScale.z);
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

}
