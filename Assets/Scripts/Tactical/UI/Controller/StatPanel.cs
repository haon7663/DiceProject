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

    public void Display(Creature creature)
    {
        _creature = creature;
        
        nameLabel.text = creature.creatureSO.name;
        HpChange();
        
        var health = _creature.GetComponent<Health>();
        health.OnHpChanged += HpChange;
    }

    private void HpChange()
    {
        var health = _creature.GetComponent<Health>();
        hpSlider.value = (float)health.curHp / health.maxHp;
        if (hpSlider.value <= 0)
            hpSlider.value = 0f;
        else if (hpSlider.value < 0.09f)
            hpSlider.value = 0.09f;
        hpLabel.text = $"{health.curHp} / {health.maxHp}";
    }
}
