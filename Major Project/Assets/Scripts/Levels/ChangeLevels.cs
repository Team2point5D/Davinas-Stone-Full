using UnityEngine;
using System.Collections;

//Marcus
public class ChangeLevels : MonoBehaviour
{


    void OnTriggerEnter(Collider col)
    {
        switch (Application.loadedLevel)
        {
            case 1:
                Application.LoadLevel(2);
                break;
            case 2:
                Application.LoadLevel(3);
                break;
            case 3:
                Application.LoadLevel(4);
                break;
            default:
                print("Error");
                break;

        }
    }
}
