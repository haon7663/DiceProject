using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectSO", menuName = "Scriptable Object/StatusEffectSO/StatStatusEffectSO")]
public class StatStatusEffectSO : StatusEffectSO
{
    [Header("능력치 변경")]
    [SerializeField] private StatType statType;
    [SerializeField] private StatModifierType statModifierType;
    [DrawIf("statModifierType", StatModifierType.Multiply)] [SerializeField] private float value;

    private StatModifier _statModifier;
    
    public override void ApplyEffect(GameObject gameObject, int stack)
    {
        SetModifier();
        gameObject.GetComponent<Creature>().Stats[statType].AddModifier(_statModifier);
    }
    
    public override void RemoveEffect(GameObject gameObject)
    {
        gameObject.GetComponent<Creature>().Stats[statType].RemoveModifier(_statModifier);
    }
    
    private void SetModifier()
    {
        _statModifier = statModifierType switch
        {
            StatModifierType.Multiply => new StatModifier(statModifierType, value),
            StatModifierType.Add => new StatModifier(statModifierType, GetCurrentStack()),
            _ => _statModifier
        };
    }
}
