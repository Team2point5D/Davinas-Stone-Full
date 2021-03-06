﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Marcus & David

public class UIFollowObj : MonoBehaviour
{

    public RectTransform canvasRectT;
    public RectTransform UIObjectRectT;
    public float positionX;
    public float positionY;
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
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, tObjectToFollow.position + new Vector3(positionX, positionY, 0f));

            UIObjectRectT.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;
        }
        else
        {
            gameObject.GetComponent<Text>().color = new Color(0, 0, 0, 0);
        }
    }
}
