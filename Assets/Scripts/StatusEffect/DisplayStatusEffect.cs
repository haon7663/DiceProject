using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStatusEffect : MonoBehaviour
{
    [SerializeField] private Image effectImage;
    [SerializeField] private TMP_Text stackTMP;

    public StatusEffectSO StatusEffectSO { get; private set; }

    public void Setup(StatusEffectSO statusEffectSO)
    {
        StatusEffectSO = statusEffectSO;
        
        effectImage.sprite = statusEffectSO.sprite;
        stackTMP.text = statusEffectSO.GetCurrentStack().ToString();
    }

    public void UpdateSetup()
    {
        stackTMP.text = StatusEffectSO.GetCurrentStack().ToString();
    }
}
