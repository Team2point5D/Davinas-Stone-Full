using UnityEngine;
using System.Collections;

//Marcus
public class ChangeLevels : MonoBehaviour
{
    void Update()
    {
        //print(Application.loadedLevel.ToString());
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
          
            if (col.GetComponent<PlayerBehaviour>().bIsGravityReversed)
            {
                col.GetComponent<PlayerBehaviour>().bIsGravityReversed = false;
                col.GetComponent<PlayerBehaviour>().bPlayerReversed = false;
                Physics.gravity = new Vector3(0, -78.48f,0);
            }

            switch (Application.loadedLevel)
            {
                case 0:
                    Application.LoadLevel(1);
                    break;
                case 1:
                    Application.LoadLevel(2);
                    break;
                case 2:
                    Application.LoadLevel(3);
                    break;
                default:
                    print("Error");
                    break;
            }



        }
    }
}
