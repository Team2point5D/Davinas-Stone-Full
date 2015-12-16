using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DetailedMainMenu : MonoBehaviour
{

    //public ChangeLevels cLevels;

    [Space(10)]

    public Canvas mainMenuCanvas;
    public Canvas challengeModeCanvas;
    public Canvas levelSelectCanvas;
    public Canvas settingsCanvas;
    public Canvas creditsCanvas;


    [Header("Level Butons")]
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;
    [Space(5)]
    public Button challengeModeButton;
    public Button levelSelectButton;


    // Use this for initialization
    void Start()
    {
//        cLevels = GetComponent<ChangeLevels>();


    }

    // Update is called once per frame
    void Update()
    {
//        switch (cLevels.hasCompletedLevelNum)
//        {
//            case 0:
//                challengeModeButton.interactable = false;
//                levelSelectButton.interactable = false;
//                break;
//            case 1:
//                challengeModeButton.interactable = true;
//                levelSelectButton.interactable = true;
//                level1Button.interactable = true;
//                break;
//            case 2:
//                challengeModeButton.interactable = true;
//                levelSelectButton.interactable = true;
//                level2Button.interactable = true;
//                break;
//            case 3:
//                challengeModeButton.interactable = true;
//                levelSelectButton.interactable = true;
//                level3Button.interactable = true;
//                break;
//            case 4:
//                challengeModeButton.interactable = true;
//                levelSelectButton.interactable = true;
//                level4Button.interactable = true;
//                break;
//            default:
//                print("ERROR!");
//                break;
//        }

    }

    // Buttons

    public void PlayButton()
    {
        Application.LoadLevel(1);
    }

    public void ChallengeButton()
    {
        print("Pressed");
    }

    public void LevelSelectButtonClick()
    {
        levelSelectCanvas.gameObject.SetActive(true);

        mainMenuCanvas.gameObject.SetActive(false);
    }

    public void SettingsButtonClick()
    {
        settingsCanvas.gameObject.SetActive(true);

        mainMenuCanvas.gameObject.SetActive(false);
    }

    public void CreditsButtonClick()
    {
        creditsCanvas.gameObject.SetActive(true);

        mainMenuCanvas.gameObject.SetActive(false);
    }

    public void MainMenuBackClick()
    {
        mainMenuCanvas.gameObject.SetActive(true);

        levelSelectCanvas.gameObject.SetActive(false);

        settingsCanvas.gameObject.SetActive(false);

        creditsCanvas.gameObject.SetActive(false);


    }

	public void Level1Button()
	{
		Application.LoadLevel (1);
	}

	public void Level2Button()
	{
		Application.LoadLevel (2);
	}

	public void Level3Button()
	{
		Application.LoadLevel (3);
	}

	public void Level4Button()
	{
		Application.LoadLevel (4);
	}

    public void QuitButton()
    {
        Application.Quit();
    }


}
