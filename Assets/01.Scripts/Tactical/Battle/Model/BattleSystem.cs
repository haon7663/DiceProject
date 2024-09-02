using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class BattleSystem
{
    public static void CompareBehaviours(Unit from, Unit to, List<Behaviour> behaviours)
    {
        var finalValue = behaviours.Sum(behaviour => behaviour.CalculateValue());

        foreach (var behaviour in behaviours)
        {
            behaviour.PerformAction(to);
        }
    }
}