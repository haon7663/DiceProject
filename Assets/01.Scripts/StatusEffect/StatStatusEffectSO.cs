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
    
    public override void ApplyEffect(Unit unit, int stack)
    {
        base.ApplyEffect(unit, stack);
        SetModifier();
        unit.Stats[statType].AddModifier(_statModifier);
    }
    
    public override void RemoveEffect(Unit unit)
    {
        unit.Stats[statType].RemoveModifier(_statModifier);
        base.RemoveEffect(unit);
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