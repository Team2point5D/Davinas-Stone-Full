using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DetailedMainMenu : MonoBehaviour
{

    public ChangeLevels cLevels;

    [Space(10)]

    public Canvas mainMenuCanvas;
    public Canvas levelSelectCanvas;

    [Header("Level Butons")]
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;
    [Space(5)]
    public Button levelSelectButton;

    // Use this for initialization
    void Start()
    {
        cLevels = GetComponent<ChangeLevels>();


    }

    // Update is called once per frame
    void Update()
    {
        switch (cLevels.hasCompletedLevelNum)
        {
            case 0:
                levelSelectButton.interactable = false;
                break;
            case 1:
                levelSelectButton.interactable = true;
                level1Button.interactable = true;
                break;
            case 2:
                levelSelectButton.interactable = true;
                level2Button.interactable = true;
                break;
            case 3:
                levelSelectButton.interactable = true;
                level3Button.interactable = true;
                break;
            case 4:
                levelSelectButton.interactable = true;
                level4Button.interactable = true;
                break;
            default:
                print("ERROR!");
                break;
        }

    }

    // Buttons
    public void LevelSelectButtonClick()
    {
        mainMenuCanvas.gameObject.SetActive(false);

        levelSelectCanvas.gameObject.SetActive(true);
    }
}
