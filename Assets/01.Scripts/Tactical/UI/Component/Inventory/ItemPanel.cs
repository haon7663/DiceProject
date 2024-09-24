﻿using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public event Action<int> Use;
    
    public Panel panel;
    public ItemSO Data { get; private set; }

    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text description;

    private int _index;
    
    public void Initialize(ItemSO itemSO, int index)
    {
        Data = itemSO;
        
        icon.sprite = itemSO.sprite;
        nameLabel.text = itemSO.itemName;
        description.text = itemSO.description;

        _index = index;
    }

    public void UseItem()
    {
        if (!Data) return;

        Use?.Invoke(_index);
        
        var behaviours = Data.behaviourInfos.CreateBehaviours();

        foreach (var behaviour in behaviours)
        {
            var target = behaviour.onSelf ? BattleController.Inst.player : BattleController.Inst.enemy;
            
            if (behaviour.GetType() == BehaviourType.Attack.GetBehaviourClass())
            {
                if (target.TryGetComponent<Health>(out var health))
                {
                    health.OnDamage(behaviour.value);
                }
            }
            
            if (behaviour.GetType() == BehaviourType.StatusEffect.GetBehaviourClass())
            {
                if (behaviour is StatusEffectBehaviour statusEffectBehaviour)
                {
                    if (target.TryGetComponent<StatusEffect>(out var statusEffect))
                    {
                        statusEffect.AddEffect(statusEffectBehaviour.statusEffectSO, behaviour.value);
                    }
                }
            }
        }

        Data.ExecuteActions();
    }
}