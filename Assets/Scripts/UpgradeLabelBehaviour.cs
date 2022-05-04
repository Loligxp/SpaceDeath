using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UpgradeLabelBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public UnityEvent onButtonDown;
    public static UpgradeLabelBehaviour selectedUpgrader;
    private RectTransform _rect;
    private bool _mouseIn;
    private Vector2 goalPosition;
    public float exitPos, enterPos, selectPos;
    

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
    }

    public void Update()
    {
        if (selectedUpgrader == this)
        {
            goalPosition = new Vector2(selectPos,_rect.localPosition.y);
        }
        else
        {
            goalPosition = _mouseIn ? new Vector2(enterPos,_rect.localPosition.y) : new Vector2(exitPos,_rect.localPosition.y);
        }

        _rect.localPosition = Vector2.Lerp(goalPosition,_rect.localPosition,0.2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mouseIn = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        selectedUpgrader = this;
        onButtonDown.Invoke();

    }
}
