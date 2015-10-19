using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Marcus
public class UIHandler : MonoBehaviour
{

    [Header("Player")]
    public PlayerBehaviour Player;

    [Header("Aimer")]
    public Texture2D texCursor;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    [Header("Abilities")]
    public Text teSelectedAbility;
    public Image imAbilityType;
    // public Sprite sTransparent;
    public Sprite sMassUp;
    public Sprite sMassDown;
    public Sprite sSonar;
    [Space(10)]
    public Sprite sScaleXUp;
    public Sprite sScaleXDown;
    public Sprite sScaleYUp;
    public Sprite sScaleYDown;
    public Sprite sScaleUp;


    // Use this for initialization
    void Start()
    {
        Cursor.SetCursor(texCursor, hotSpot, cursorMode);
        teSelectedAbility.text = "";

        //imAbilityType.overrideSprite = sTransparent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.bIsHeavySelected)
        {
            teSelectedAbility.text = "Mass";
        }

        if (Player.bCanUseSonar)
        {
            teSelectedAbility.text = "Sonar";

        }

        if (Player.bCanUseScale)
        {
            teSelectedAbility.text = "Scale";
        }



    }

    public void SwitchMassUI()
    {
        if (Player.bIsHeavySelected)
        {
            imAbilityType.overrideSprite = sMassUp;

        }
        else
        {
            imAbilityType.overrideSprite = sMassDown;
        }

        //teSelectedAbility.text = "Mass";
    }

    public void SwitchSonarUI()
    {
        if (Player.bCanUseSonar)
        {
            imAbilityType.overrideSprite = sSonar;

        }

        teSelectedAbility.text = "Sonar";


    }

    public void SwitchScaleUI()
    {
        if (Player.bCanUseScale)
        {
            imAbilityType.overrideSprite = sScaleXUp;
        }

        teSelectedAbility.text = "Scale";
    }
}
