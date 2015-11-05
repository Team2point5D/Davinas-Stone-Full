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
    private bool bIsTimer1;
    private Vector3 vStartingSize;
    public float scaleXUpSize = 1;
    public float scaleXDownSize = 1;

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

        if (fScaleTimer >= 1f)
        {
            bIsTimer1 = true;
        }
        else
        {
            bIsTimer1 = false;
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
        //transform.localScale = Vector3.Lerp (new Vector3(scaleUSize, transform.localScale.y, transform.localScale.z),
        //                                     new Vector3(scaleSSize, transform.localScale.y, transform.localScale.z),
        //                                     fScaleTimer);
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
        //if (bIsScaleUp == true)
        //{
        //    //fScaleTimer += 5 * Time.deltaTime;
        //    ChangeStateToExpanded();
        //}
        //else
        //{
        //    fScaleTimer += 5 * Time.deltaTime;
        //    gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        //}
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
        
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
    void ChangeStateToContracted()
    {
        transform.localScale = Vector3.Lerp(new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z),
                                             new Vector3(scaleXDownSize, transform.localScale.y, transform.localScale.z),
                                             fScaleTimer);
        fScaleTimer += Time.deltaTime;
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

}
