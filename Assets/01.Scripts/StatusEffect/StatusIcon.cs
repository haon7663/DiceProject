using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StatusIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        BattleController.Inst.statusEffectExplainPanel.Show(StatusEffectSO);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BattleController.Inst.statusEffectExplainPanel.Hide();
    }
}
