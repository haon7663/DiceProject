using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StatusIcon : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text stackLabel;

    public StatusEffectSO StatusEffectSO { get; private set; }

    public void Init(StatusEffectSO statusEffectSO)
    {
        StatusEffectSO = statusEffectSO;
        
        icon.sprite = statusEffectSO.sprite;
        stackLabel.text = statusEffectSO.GetCurrentStack().ToString();
    }

    public void UpdateSetup()
    {
        stackLabel.text = StatusEffectSO.GetCurrentStack().ToString();
    }
}
