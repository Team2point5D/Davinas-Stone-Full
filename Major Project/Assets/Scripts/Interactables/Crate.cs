using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//David
[RequireComponent(typeof(Rigidbody))]
public class Crate : MonoBehaviour {

    [Header("Interaction")]
    public bool bIsPickedUp = false;
    public bool bIsClickable = true;
    private string sCurrentGround;
    private float fGroundRayDetectionDistance = 1f;
    private bool bIsGrounded;
    private float fCratesZAxis;

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
    public float fScaleSpeed;
    public bool bIsObjectExpanded = false;
    public bool bIsObjectContracted = false;
    public float fScaleXUpSize = 1;
    public float fScaleXDownSize = 1;
    public float fScaleYUpSize = 1;
    public float fScaleYDownSize = 1;
    public float fScaleXYUpSize = 1;
    public float fScaleXYDownSize = 1;
    private float fLastState = -1f;
    private float fStartingScaleX;
    private float fStartingScaleY;
    private float fStartingScaleZ;
    private bool bIsChangingScale;
	private float fScaleTimer = 0;
    private bool bIsScaling = false;
    private Vector3 vCurrentSize;
    private Vector3 vStartingSize;
    private Vector3 vEndingSize;

    [Header("References")]
    public Transform PlayerHolder;
	public PlayerBehaviour PlayerBehaviour;
    private Rigidbody myRigidBody;

	void Start ()
	{
		//PlayerBehaviour = GameObject.FindWithTag ("Player").GetComponent<PlayerBehaviour>();
        //PlayerHolder = GameObject.FindWithTag("Holder").GetComponent<Transform>();
        myRigidBody = this.gameObject.GetComponent<Rigidbody>();

        vStartingSize = gameObject.transform.localScale;
        fStartingScaleX = transform.localScale.x;
        fStartingScaleY = transform.localScale.y;
        fStartingScaleZ = transform.localScale.z;
        myRenderer = gameObject.GetComponent<Renderer>();
        if (bIsObjectZeroMass)
        {
            mStartingMaterial = mZeroMass;
            mEndingMaterial = mZeroMass;
        }
        else
        {
            mStartingMaterial = mStandard;
            mEndingMaterial = mStandard;
        }

        fCratesZAxis = transform.position.z;
	}

	void Update () 
	{
		fScaleTimer = Mathf.Clamp (fScaleTimer, 0, 1);
        fMaterialTimer = Mathf.Clamp(fMaterialTimer, 0, 1);

        if (bIsGrounded)
        {
           // print("Grounded");

            transform.position = new Vector3(transform.position.x, transform.position.y, fCratesZAxis);

            myRigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
        }
        else
        {
            myRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
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

		if(bIsObjectZeroMass)
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

        if (PlayerBehaviour.bIsGravityReversed == false)
        {
            RaycastHit hitPoint;

            if (Physics.Raycast(transform.position, Vector3.down, out hitPoint, fGroundRayDetectionDistance))
            {
                bIsGrounded = true;
                sCurrentGround = hitPoint.transform.tag;
            }
            else
            {
                bIsGrounded = false;
                sCurrentGround = null;
            }
        }
        else
        {
            RaycastHit hitPoint;
            if (Physics.Raycast(transform.position, Vector3.up, out hitPoint, fGroundRayDetectionDistance))
            {
                bIsGrounded = true;
                sCurrentGround = hitPoint.transform.tag;
            }
            else
            {
                bIsGrounded = false;
                sCurrentGround = null;
            }
        }

	}

	public void ChangeMass ()
	{
		if(!bIsObjectZeroMass && !bIsChangingMass)
		{
            Debug.Log("Change Mass");
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
        if (!bIsScaling && !bIsChangingScale)
        {
            Debug.Log("Change Scale");
            if (PlayerBehaviour.fScaleState == 1 ||
                PlayerBehaviour.fScaleState == 3 ||
                PlayerBehaviour.fScaleState == 5)
            {
                if (fLastState == 1)
                {
                    if (PlayerBehaviour.fScaleState == 3)
                    {
                        bIsChangingScale = true;
                    }
                    else if (PlayerBehaviour.fScaleState == 5)
                    {
                        bIsChangingScale = true;
                    }

                }
                else if (fLastState == 3)
                {
                    if (PlayerBehaviour.fScaleState == 1)
                    {
                        bIsChangingScale = true;
                    }
                    else if (PlayerBehaviour.fScaleState == 5)
                    {
                        bIsChangingScale = true;
                    }

                }
                if (fLastState == 5)
                {
                    if (PlayerBehaviour.fScaleState == 1)
                    {
                        bIsChangingScale = true;
                    }
                    else if (PlayerBehaviour.fScaleState == 3)
                    {
                        bIsChangingScale = true;
                    }

                }
                else
                {
                    bIsObjectExpanded = !bIsObjectExpanded;
                    bIsObjectContracted = false;
                    bIsChangingScale = true;
                }
                fLastState = PlayerBehaviour.fScaleState;
            }
            else if (PlayerBehaviour.fScaleState == 0 ||
                     PlayerBehaviour.fScaleState == 2 ||
                     PlayerBehaviour.fScaleState == 4)
            {
                if (fLastState == 0)
                {
                    if (PlayerBehaviour.fScaleState == 2)
                    {
                        bIsChangingScale = true;
                    }
                    else if (PlayerBehaviour.fScaleState == 4)
                    {
                        bIsChangingScale = true;
                    }

                }
                else if (fLastState == 2)
                {
                    if (PlayerBehaviour.fScaleState == 0)
                    {
                        bIsChangingScale = true;
                    }
                    else if (PlayerBehaviour.fScaleState == 4)
                    {
                        bIsChangingScale = true;
                    }

                }
                if (fLastState == 4)
                {
                    if (PlayerBehaviour.fScaleState == 0)
                    {
                        bIsChangingScale = true;
                    }
                    else if (PlayerBehaviour.fScaleState == 2)
                    {
                        bIsChangingScale = true;
                    }

                }
                else
                {
                    bIsObjectContracted = !bIsObjectContracted;
                    bIsObjectExpanded = false;
                    bIsChangingScale = true;
                }
                fLastState = PlayerBehaviour.fScaleState;
            }
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
        if (PlayerBehaviour.fScaleState == 1)
        {
            vEndingSize = new Vector3(fScaleXUpSize, fStartingScaleY, fStartingScaleZ);
        }
        else if (PlayerBehaviour.fScaleState == 3)
        {
            vEndingSize = new Vector3(fStartingScaleX, fScaleYUpSize, fStartingScaleZ);
        }
        else if (PlayerBehaviour.fScaleState == 5)
        {
            vEndingSize = new Vector3(fScaleXYUpSize, fScaleXYUpSize, fScaleXYUpSize);
        }
        mEndingMaterial = mExpanded;
    }
    void ChangeStateToContracted()
    {
        bIsScaling = true;
        bIsChangingMaterial = true;
        bIsChangingScale = false;
        vCurrentSize = transform.localScale;
        if (PlayerBehaviour.fScaleState == 0)
        {
            vEndingSize = new Vector3(fScaleXDownSize, fStartingScaleY, fStartingScaleZ);
        }
        else if (PlayerBehaviour.fScaleState == 2)
        {
            vEndingSize = new Vector3(fStartingScaleX, fScaleYDownSize, fStartingScaleZ);
        }
        else if (PlayerBehaviour.fScaleState == 4)
        {
            vEndingSize = new Vector3(fScaleXYDownSize, fScaleXYDownSize, fScaleXYDownSize);
        }
        mEndingMaterial = mContracted;
    }

    public void Landing()
    {
        if (sCurrentGround == "DirtFloor")
        {
            FMOD_StudioSystem.instance.PlayOneShot("event:/Movement/Jump/dirtLanding", transform.position, 0.9f);
        }
        else if (sCurrentGround == "GravelFloor")
        {
            FMOD_StudioSystem.instance.PlayOneShot("event:/Movement/Jump/gravelLanding", transform.position, 0.9f);
        }
        else if (sCurrentGround == "StoneFloor")
        {
            FMOD_StudioSystem.instance.PlayOneShot("event:/Movement/Jump/stoneLanding", transform.position, 0.9f);
        }
        else if (sCurrentGround == "UnevenStoneFloor")
        {
            FMOD_StudioSystem.instance.PlayOneShot("event:/Movement/Jump/unevenStoneLanding", transform.position, 0.9f);
        }
        else if (sCurrentGround == "WoodFloor")
        {
            FMOD_StudioSystem.instance.PlayOneShot("event:/Movement/Jump/dirtLanding", transform.position, 0.9f);
        }
    }

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.tag.Contains("Floor"))
        {
            Landing();
        }
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

    void OnMouseDown ()
    {
        if (bIsClickable)
        {
            Debug.Log("Clicked");
            if (PlayerBehaviour.bIsMass)
            {
                ChangeMass();
                Debug.Log("Mass");
            }
            else if (PlayerBehaviour.bIsScale)
            {
                ChangeScale();
                Debug.Log("Scale");
            }
        }
    }

}
