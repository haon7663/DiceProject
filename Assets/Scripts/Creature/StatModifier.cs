using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatModifierType { Add = 100, Multiply = 200 }

[Serializable]
public class StatModifier
{
    public StatModifierType statModifierType;
    public float value;

    public StatModifier(StatModifierType statModifierType, float value)
    {
        this.statModifierType = statModifierType;
        this.value = value;
    }
}
