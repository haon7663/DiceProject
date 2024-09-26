using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectExplainPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text description;

    private bool _isShown;

    [SerializeField] private Vector2 offset;

    public void Show(StatusEffectSO statusEffectSO)
    {
        icon.sprite = statusEffectSO.sprite;
        nameLabel.text = statusEffectSO.label;
        description.text = statusEffectSO.description;

        canvasGroup.alpha = 1;
        _isShown = true;
    }
    
    public void Hide()
    {
        canvasGroup.alpha = 0;
        _isShown = false;
    }

    private void Update()
    {
        if (_isShown)
        {
            if (Input.mousePosition.x > 0)
                transform.position = (Vector2)Input.mousePosition + offset * new Vector2(1, 1);
            else
                transform.position = (Vector2)Input.mousePosition + offset * new Vector2(-1, 1);
        }
        
    }
}
