using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatStatusEffectSO", menuName = "Scriptable Object/StatusEffectSO/StatStatusEffectSO")]
public class StatStatusEffectSO : StatusEffectSO
{
    [Header("능력치 변경")]
    [SerializeField] private StatType statType;
    [SerializeField] private StatModifierType statModifierType;
    [DrawIf("statModifierType", StatModifierType.Add)] [SerializeField] private bool useStack;
    [DrawIf("useStack", false)] [SerializeField] private float value;

    private StatModifier _statModifier;
    
    public override void ApplyEffect(Creature creature, int stack)
    {
        base.ApplyEffect(creature, stack);
        SetModifier();
        creature.Stats[statType].AddModifier(_statModifier);
    }
    
    public override void RemoveEffect(Creature creature)
    {
        creature.Stats[statType].RemoveModifier(_statModifier);
        base.RemoveEffect(creature);
    }
    
    private void SetModifier()
    {
        _statModifier = statModifierType switch
        {
            StatModifierType.Multiply => new StatModifier(statModifierType, value),
            StatModifierType.Add => new StatModifier(statModifierType, useStack ? GetCurrentStack() : value),
            _ => _statModifier
        };
    }
}
