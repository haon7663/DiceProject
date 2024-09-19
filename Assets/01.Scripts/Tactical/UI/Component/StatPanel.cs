using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class StatPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TMP_Text hpLabel;
    
    [SerializeField] private Transform effectParent;
    [SerializeField] private StatusIcon effectPrefab;
    private List<StatusIcon> _statusIcons;

    private Unit _unit;

    public void Connect(Unit unit)
    {
        _unit = unit;
        nameLabel.text = unit.unitSO.name;

        if (_unit.TryGetComponent<Health>(out var health))
        {
            health.OnHpChanged += HpChange;
            HpChange(0);
        }
        if (_unit.TryGetComponent<StatusEffect>(out var status))
        {
            status.OnStatusChanged += StatusChange;
            StatusChange();
        }
    }

    public void HpChange(int value)
    {
        if (!_unit.TryGetComponent<Health>(out var health))
            return;
        
        hpSlider.value = (float)health.curHp / health.maxHp;
        hpLabel.text = $"{health.curHp} / {health.maxHp}";

        hpSlider.value = hpSlider.value switch
        {
            <= 0 => 0f,
            <= 0.065f => 0.065f,
            _ => hpSlider.value
        };
    }

    private void StatusChange()
    {
        if (!_unit.TryGetComponent<StatusEffect>(out var status))
            return;

        if(_statusIcons is { Count: > 0 })
            _statusIcons.ForEach(icon => Destroy(icon.gameObject));
        _statusIcons = new List<StatusIcon>();

        foreach (var effect in status.enabledEffects)
        {
            var statusIcon = Instantiate(effectPrefab, effectParent);
            statusIcon.Init(effect);
            _statusIcons.Add(statusIcon);
        }
    }
}
