using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TMP_Text hpLabel;

    private Creature _creature;

    public void Connect(Creature creature)
    {
        _creature = creature;

        nameLabel.text = creature.creatureSO.name;

        if (_creature.TryGetComponent<Health>(out var health))
        {
            health.OnHpChanged += HpChange;
            HpChange();
        }
    }

    private void HpChange()
    {
        if (!_creature.TryGetComponent<Health>(out var health))
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
}
