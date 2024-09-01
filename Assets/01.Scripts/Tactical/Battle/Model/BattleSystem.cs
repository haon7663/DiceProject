using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class BattleSystem
{
    public static (bool, int) CompareBehaviours(List<Behaviour> behaviours)
    {
        var finalValue = behaviours.Sum(behaviour => behaviour.CalculateValue());

        foreach (var behaviour in behaviours)
        {
            if (!behaviour.CalculateResult(finalValue))
                return (false, 0);
        }
        
        return (true, finalValue);
    }
}