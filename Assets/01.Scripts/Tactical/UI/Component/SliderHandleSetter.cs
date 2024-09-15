using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandleSetter : MonoBehaviour
{
    [SerializeField] private RectTransform fillRect;
    [SerializeField] private RectTransform handleRect;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetHandleRect);
    }
    
    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetHandleRect);
    }

    private void SetHandleRect(float value)
    {
        var handleX = Mathf.Lerp(30f, -30f, value);
        handleRect.anchoredPosition = new Vector2(handleX, 0);
        
        var fillX = Mathf.Lerp(30f, 0, value);
        fillRect.anchoredPosition = new Vector2(fillX, 0);
        
        var sizeDeltaX = Mathf.Lerp(60f, 0, value);
        fillRect.sizeDelta = new Vector2(sizeDeltaX, 0);
    }
}
