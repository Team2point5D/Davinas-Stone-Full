using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandler : MonoBehaviour {

    [Header("Player")]
    public PlayerBehaviour Player;

    [Header("Aimer")]
    public Texture2D texCursor;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    [Header("Abilities")]
    public Text teSelectedAbility;
    public Image imAbilityType;
    public Sprite sTransparent;
    public Sprite sMassUp;
    public Sprite sMassDown;

	// Use this for initialization
	void Start () 
    {
        Cursor.SetCursor(texCursor, hotSpot, cursorMode);
        teSelectedAbility.text = "";
        imAbilityType.overrideSprite = sTransparent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SwitchMassUI()
    {
        if (Player.bIsHeavySelected)
        {
            imAbilityType.overrideSprite = sMassUp;
            //FMOD_StudioSystem.instance.PlayOneShot("event:/Sound effects/Mass up", transform.position);
        }
        else
        {
            imAbilityType.overrideSprite = sMassDown;
            // FMOD_StudioSystem.instance.PlayOneShot("event:/Sound effects/Mass down", transform.position);
        }
    }
}
