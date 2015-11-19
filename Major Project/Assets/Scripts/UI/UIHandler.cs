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
    private Vector2 hotSpot;
    public CursorMode cursorMode = CursorMode.Auto;

    [Header("Abilities")]
    public Text teAnimationState;
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
    public Sprite sScaleXYUp;
    public Sprite sScaleXYDown;


    // Use this for initialization
    void Start()
    {
        hotSpot = new Vector2(40f, 40f);
        Cursor.SetCursor(texCursor, hotSpot, cursorMode);
        teSelectedAbility.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.bIsMass)
        {
            if (Player.bIsHeavySelected)
            {
                imAbilityType.overrideSprite = sMassUp;

            }
            else
            {
                imAbilityType.overrideSprite = sMassDown;
            }
        }

        if (Player.bIsScale)
        {
            if (Player.fScaleState == 0)
            {
                imAbilityType.overrideSprite = sScaleXDown;
            }
            else if (Player.fScaleState == 1)
            {
                imAbilityType.overrideSprite = sScaleXUp;
            }
            else if (Player.fScaleState == 2)
            {
                imAbilityType.overrideSprite = sScaleYDown;
            }
            else if (Player.fScaleState == 3)
            {
                imAbilityType.overrideSprite = sScaleYUp;
            }
            else if (Player.fScaleState == 4)
            {
                imAbilityType.overrideSprite = sScaleXYDown;
            }
            else if (Player.fScaleState == 5)
            {
                imAbilityType.overrideSprite = sScaleXYUp;
            }
        }
    }
}
