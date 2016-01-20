using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFollowObj : MonoBehaviour
{

    public RectTransform canvasRectT;
    public RectTransform UIObjectRectT;
    Transform tObjectToFollow;

    // Use this for initialization
    void Start()
    {
        tObjectToFollow = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (tObjectToFollow != null)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, tObjectToFollow.position + new Vector3(0f, 0f, 0f));

            UIObjectRectT.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;
        }
        else
        {
            gameObject.GetComponent<Text>().color = new Color(0, 0, 0, 0);
        }
    }
}
