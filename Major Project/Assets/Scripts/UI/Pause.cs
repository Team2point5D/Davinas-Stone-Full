using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    private bool bIsGamePaused;
	
	void Update ()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (bIsGamePaused)
        {
            Debug.Log("Unpause");
            Time.timeScale = 1;
            bIsGamePaused = false;
        }
        else
        {
            Debug.Log("Pause");
            Time.timeScale = 0;
            bIsGamePaused = true;
        }
    }
}
