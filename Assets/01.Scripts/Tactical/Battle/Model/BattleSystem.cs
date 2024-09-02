using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class BattleSystem
{
    public static (bool, int) CompareBehaviours(List<Behaviour> behaviours)
    {
        var finalValue = behaviours.Sum(behaviour => behaviour.CalculateValue());
        var finalResult = behaviours.Any(behaviour => behaviour.CalculateResult(finalValue));
        
        return (finalResult, finalValue);
    }
}