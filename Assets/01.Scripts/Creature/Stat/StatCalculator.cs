using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatCalculator
{
    //공격
    public static int CalculateOffence(Unit unit, int value)
    {
        var finalValue = unit.Stats[StatType.GetDamage].GetValue(value);
        return finalValue;
    }
    public static int CalculateOffence(Unit unit, Unit targetUnit, int value)
    {
        var finalValue = CalculateOffence(unit, value);
        return targetUnit.Stats[StatType.TakeDamage].GetValue(finalValue);
    }
    
    //방어
    public static int CalculateDefence(Unit unit, int value)
    {
        var finalValue = value;
        return finalValue;
    }
    public static int CalculateDefence(Unit unit, Unit targetUnit, int value)
    {
        var finalValue = CalculateDefence(unit, value);
        return targetUnit.Stats[StatType.TakeDefence].GetValue(finalValue);
    }
    
    //회복
    public static int CalculateRecovery(Unit unit, int value)
    {
        var finalValue = value;
        return finalValue;
    }
    public static int CalculateRecovery(Unit unit, Unit targetUnit, int value)
    {
        var finalValue = CalculateRecovery(unit, value);
        return targetUnit.Stats[StatType.TakeRecovery].GetValue(finalValue);
    }
}
