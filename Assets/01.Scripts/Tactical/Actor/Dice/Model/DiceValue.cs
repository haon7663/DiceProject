using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DiceValue
{
    public static int GetDiceValue(this DiceType diceType)
    {
        return Random.Range(1, GetDiceMaxValue(diceType) + 1);;
    }
    
    public static int GetDiceMaxValue(this DiceType diceType)
    {
        return diceType switch
        {
            DiceType.Four => 4,
            DiceType.Six => 6,
            DiceType.Eight => 8,
            DiceType.Twelve => 12,
            DiceType.Twenty => 20,
            _ => 0
        };
    }
}
