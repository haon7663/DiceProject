using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class UnitStat
{
    private readonly List<StatModifier> _statModifiers = new List<StatModifier>();

    public int GetValue(float value)
    {
        var finalValue = value;
        finalValue += _statModifiers.FindAll(stat => stat.statModifierType == StatModifierType.Add).Sum(statModifier => statModifier.value);
        finalValue = _statModifiers.FindAll(stat => stat.statModifierType == StatModifierType.Multiply).Aggregate(finalValue, (current, statModifier) => current * statModifier.value);
        return Mathf.RoundToInt(finalValue);
    }
    
    public void AddModifier(StatModifier modifier)
    {
        _statModifiers.Add(modifier);
    }
    public void RemoveModifier(StatModifier modifier)
    {
        _statModifiers.Remove(modifier);
    }
    public void RemoveAllModifier()
    {
        _statModifiers.Clear();
    }
}
