using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//David
[RequireComponent(typeof(Rigidbody))]
public class Crate : MonoBehaviour {

    [Header("Interaction")]
    public bool bIsPickedUp = false;

    [Header("Materials")]
    public float fMaterialSpeed;
    public Material mStandard;
    public Material mZeroMass;
    public Material mHeavy;
    public Material mLight;
    public Material mExpanded;
    public Material mContracted;
    private bool bIsChangingMaterial = false;
    private Renderer myRenderer;
    private Material mStartingMaterial;
    private Material mEndingMaterial;
    private float fMaterialTimer = 0;

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
    public float fScaleSpeed;
	private float fScaleTimer = 0;
    private bool bIsScaling = false;
    private Vector3 vCurrentSize;
    private Vector3 vStartingSize;
    private Vector3 vEndingSize;
    public float fScaleXUpSize = 1;
    public float fScaleDownSize = 1;

    [Header("References")]
    public Transform PlayerHolder;
	private PlayerBehaviour PlayerBehaviour;
    private Rigidbody myRigidBody;

	void Start ()
	{
		PlayerBehaviour = GameObject.FindWithTag ("Player").GetComponent<PlayerBehaviour>();
        //PlayerHolder = GameObject.FindWithTag("Holder").GetComponent<Transform>();
        myRigidBody = this.gameObject.GetComponent<Rigidbody>();

        vStartingSize = gameObject.transform.localScale;
        myRenderer = gameObject.GetComponent<Renderer>();
        mStartingMaterial = mStandard;
        mEndingMaterial = mStandard;
	}

	void Update () 
	{
		fScaleTimer = Mathf.Clamp (fScaleTimer, 0, 1);
        fMaterialTimer = Mathf.Clamp(fMaterialTimer, 0, 1);

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

        if (!bIsObjectHeavy && !bIsObjectLight && bIsChangingMass)
        {
            ChangeMassToRegular();
        }

        if (!bIsObjectContracted && !bIsObjectExpanded && bIsChangingScale)
        {
            ChangeScaleToRegular();
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
            ChangeStateToContracted();
        }
        else if (bIsObjectExpanded && !bIsObjectContracted && bIsChangingScale)
        {
            ChangeStateToExpanded();
        }

        if (bIsChangingMaterial)
        {
            fMaterialTimer += fMaterialSpeed * Time.deltaTime;

            myRenderer.material.Lerp(mStartingMaterial,
                                     mEndingMaterial,
                                     fMaterialTimer);

            if (fMaterialTimer >= 1f)
            {
                bIsChangingMaterial = false;
                mStartingMaterial = mEndingMaterial;
                fMaterialTimer = 0f;
            }
        }

        if (bIsScaling)
        {
            fScaleTimer += fScaleSpeed * Time.deltaTime;

            transform.localScale = Vector3.Lerp(vCurrentSize,
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
        }
        else
        {
            bIsObjectContracted = !bIsObjectContracted;
            bIsObjectExpanded = false;
            bIsChangingScale = true;
        }
    }

    //Change states based on what is shot at the crate

    void ResetStates()
    {
        bIsObjectHeavy = false;
        bIsObjectLight = false;
        bIsObjectZeroMass = false;
    }

    void ChangeScaleToRegular ()
    {
        bIsScaling = true;
        bIsChangingMaterial = true;
        vCurrentSize = transform.localScale;
        vEndingSize = vStartingSize;
        bIsChangingScale = false;
        if (bIsObjectHeavy)
        {
            mEndingMaterial = mHeavy;
        }
        else if (bIsObjectLight)
        {
            mEndingMaterial = mLight;
        }
        else
        {
            mEndingMaterial = mStandard;
        }
    }

    void ChangeMassToRegular()
    {
        bIsChangingMass = false;
        bIsChangingMaterial = true;
        gameObject.GetComponent<Rigidbody>().mass = 5;
        mEndingMaterial = mStandard;
        if (bIsObjectContracted)
        {
            mEndingMaterial = mContracted;
        }
        else if (bIsObjectExpanded)
        {
            mEndingMaterial = mExpanded;
        }
        else
        {
            mEndingMaterial = mStandard;
        }
    }

    void ChangeStateToLight ()
    {
        ResetStates();
        bIsObjectLight = true;
        bIsChangingMass = false;
        bIsChangingMaterial = true;
        gameObject.GetComponent<Rigidbody>().mass = 2.5f;
        mEndingMaterial = mLight;
    }

    void ChangeStateToHeavy ()
    {
        ResetStates();
        bIsObjectHeavy = true;
        bIsChangingMass = false;
        bIsChangingMaterial = true;
        gameObject.GetComponent<Rigidbody>().mass = 10;
        mEndingMaterial = mHeavy;
    }

    void ChangeStateToZeroMass ()
    {
        ResetStates();
        bIsObjectZeroMass = true;
        bIsChangingMass = false;
        bIsChangingMaterial = true;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        mEndingMaterial = mZeroMass;
    }

    void ChangeStateToExpanded()
    {
        bIsScaling = true;
        bIsChangingMaterial = true;
        bIsChangingScale = false;
        vCurrentSize = transform.localScale;
        vEndingSize = new Vector3(fScaleXUpSize, transform.localScale.y, transform.localScale.z);
        mEndingMaterial = mExpanded;
    }
    void ChangeStateToContracted()
    {
        bIsScaling = true;
        bIsChangingMaterial = true;
        bIsChangingScale = false;
        vCurrentSize = transform.localScale;
        vEndingSize = new Vector3(fScaleDownSize, transform.localScale.y, transform.localScale.z);
        mEndingMaterial = mContracted;
    }

    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Mass Bullet")
        {
            ChangeMass();
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Scale Bullet")
        {
            ChangeScale();
            Destroy(col.gameObject);
        }
    }

}
