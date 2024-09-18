using System;
using UnityEngine;

public class DiceReward : Reward
{
    public DiceType diceType;
    public int count;

    public DiceReward(DiceType diceType, int count)
    {
        this.diceType = diceType;
        this.count = count;
    }
    
    public override Sprite GetSprite()
    {
        return Resources.Load<Sprite>($"Rewards/Dices/{diceType}");
    }

    public override string GetLabel()
    {
        var diceName = diceType switch
        {
            DiceType.Four => "4면체",
            DiceType.Six => "6면체",
            DiceType.Eight => "8면체",
            DiceType.Twelve => "12면체",
            DiceType.Twenty => "20면체",
            _ => throw new ArgumentOutOfRangeException()
        };
        return $"{diceName} 주사위 x{count}";
    }

    public override void Execute()
    {
        DataManager.Inst.playerData.Dices[diceType] += count;
    }
}