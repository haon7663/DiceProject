using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatCalculator
{
    //공격
    public static int CalculateOffence(Creature creature, int value)
    {
        var finalValue = creature.Stats[StatType.GetDamage].GetValue(value);
        return finalValue;
    }
    public static int CalculateOffence(Creature creature, Creature targetCreature, int value)
    {
        var finalValue = CalculateOffence(creature, value);
        return targetCreature.Stats[StatType.TakeDamage].GetValue(finalValue);
    }
    
    //방어
    public static int CalculateDefence(Creature creature, int value)
    {
        var finalValue = value;
        return finalValue;
    }
    public static int CalculateDefence(Creature creature, Creature targetCreature, int value)
    {
        var finalValue = CalculateDefence(creature, value);
        return targetCreature.Stats[StatType.TakeDefence].GetValue(finalValue);
    }
    
    //회복
    public static int CalculateRecovery(Creature creature, int value)
    {
        var finalValue = value;
        return finalValue;
    }
    public static int CalculateRecovery(Creature creature, Creature targetCreature, int value)
    {
        var finalValue = CalculateRecovery(creature, value);
        return targetCreature.Stats[StatType.TakeRecovery].GetValue(finalValue);
    }
}
