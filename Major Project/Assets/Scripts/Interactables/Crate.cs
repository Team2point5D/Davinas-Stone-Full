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
    private bool bIsChangingMass = false;

    [Header("Scale")]
    public bool bIsObjectExpanded = false;
    public bool bIsObjectContracted = false;
    public bool bIsScaleUp = true;
    private bool bIsChangingScale;
    public float fSpeedScale;

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

		if(bIsObjectZeroMass && !bIsObjectLight && !bIsObjectHeavy)
		{
            ChangeStateToZeroMass();
		}

        if (!bIsObjectHeavy && !bIsObjectLight && !bIsObjectExpanded && !bIsObjectContracted && (bIsChangingMass || bIsChangingScale))
        {
            ChangeStateToRegular();
        }
        else if (bIsObjectHeavy && bIsChangingMass)
        {
            ChangeStateToHeavy();
        }
        else if(bIsObjectLight && bIsChangingMass)
        {
            ChangeStateToLight();
        }

        if (bIsObjectContracted && !bIsObjectExpanded && bIsChangingScale)
        {
            Debug.Log("Contracting");
            ChangeStateToContracted();
        }
        else if (bIsObjectExpanded && !bIsObjectContracted && bIsChangingScale)
        {
            Debug.Log("Expanding");
            ChangeStateToExpanded();
        }


        if (bIsScaling)
        {
            fScaleTimer += Time.deltaTime;

            transform.localScale = Vector3.Lerp(transform.localScale,
                                                vEndingSize,
                                                fScaleTimer);
            if (fScaleTimer >= 1f)
            {
                bIsScaling = false;
                fScaleTimer = 0f;
            }
        }
	}

	public void ChangeMass ()
	{
		if(!bIsObjectZeroMass)
		{
			if(PlayerBehaviour.bIsHeavySelected)
			{
                bIsObjectHeavy = !bIsObjectHeavy;
                bIsObjectLight = false;
                bIsChangingMass = true;
			}
			else
			{
                bIsObjectLight = !bIsObjectLight;
                bIsObjectHeavy = false;
                bIsChangingMass = true;
			}
		}
	}

    public void ChangeScale()
    {
        if (PlayerBehaviour.bIsScalingUp)
        {
            bIsObjectExpanded = !bIsObjectExpanded;
            bIsObjectContracted = false;
            bIsChangingScale = true;
            Debug.Log("Expand");
        }
        else
        {
            bIsObjectContracted = !bIsObjectContracted;
            bIsObjectExpanded = false;
            bIsChangingScale = true;
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
        bIsChangingMass = false;
        bIsChangingScale = false;
        gameObject.GetComponent<Rigidbody>().mass = 5;
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    void ChangeStateToLight ()
    {
        ResetStates();
        bIsObjectLight = true;
        bIsChangingMass = false;
        gameObject.GetComponent<Rigidbody>().mass = 2.5f;
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    void ChangeStateToHeavy ()
    {
        ResetStates();
        bIsObjectHeavy = true;
        bIsChangingMass = false;
        gameObject.GetComponent<Rigidbody>().mass = 10;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    void ChangeStateToZeroMass ()
    {
        ResetStates();
        bIsObjectZeroMass = true;
        bIsChangingMass = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }

    void ChangeStateToExpanded()
    {
        bIsScaling = true;
        bIsChangingScale = false;
        vEndingSize = new Vector3(fScaleXUpSize, transform.localScale.y, transform.localScale.z);
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
    void ChangeStateToContracted()
    {
        bIsScaling = true;
        bIsChangingScale = false;
        vEndingSize = new Vector3(fScaleDownSize, transform.localScale.y, transform.localScale.z);
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

}
