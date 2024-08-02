using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DisplayStatusEffect : MonoBehaviour
{
    [SerializeField] private Image effectImage;
    [SerializeField] private TMP_Text stackText;

    public StatusEffectSO StatusEffectSO { get; private set; }

    public void Setup(StatusEffectSO statusEffectSO)
    {
        StatusEffectSO = statusEffectSO;
        
        effectImage.sprite = statusEffectSO.sprite;
        stackText.text = statusEffectSO.GetCurrentStack().ToString();
    }

    public void UpdateSetup()
    {
        stackText.text = StatusEffectSO.GetCurrentStack().ToString();
    }
}
