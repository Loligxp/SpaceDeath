using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTextColoring : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private EventSystem _EV;
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private Color neutralColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color clickedColor;

    private bool _mouseDown;
    
    private void Awake()
    {
        _EV = EventSystem.current;;
    }

    public void Update()
    {
        if(_mouseDown)
            return;

        targetText.color = _EV.currentSelectedGameObject == this.gameObject ? selectedColor : neutralColor;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        _mouseDown = true;
        targetText.color = clickedColor;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _mouseDown = false;
        
        targetText.color = neutralColor;

    }
}
