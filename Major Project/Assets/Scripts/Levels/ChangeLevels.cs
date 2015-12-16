using UnityEngine;
using System.Collections;

//Marcus
public class ChangeLevels : MonoBehaviour
{

    public int hasCompletedLevelNum;

    void Start()
    {
        hasCompletedLevelNum = PlayerPrefs.GetInt("hasCompletedLevelNum");
    }

    void Update()
    {
        //print(Application.loadedLevel.ToString());

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.LoadLevel (0);
		}
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {

            if (col.GetComponent<PlayerBehaviour>().bIsGravityReversed)
            {
                col.GetComponent<PlayerBehaviour>().bIsGravityReversed = false;
                col.GetComponent<PlayerBehaviour>().bPlayerReversed = false;
                Physics.gravity = new Vector3(0, -78.48f, 0);
            }

            switch (Application.loadedLevel)
            {
                case 0:
                    Application.LoadLevel(1);
                    hasCompletedLevelNum++;
                    break;
                case 1:
                    Application.LoadLevel(3);
                    hasCompletedLevelNum++;
                    break;
                case 2:
                    Application.LoadLevel(4);
                    hasCompletedLevelNum++;
                    break;
                default:
                    print("Error");
                    break;
            }



        }
    }
}
