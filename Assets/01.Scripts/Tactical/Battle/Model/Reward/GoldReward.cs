using UnityEngine;

public class GoldReward : Reward
{
    public int gold;
    
    public GoldReward(int gold)
    {
        this.gold = gold;
    }

    public override Sprite GetSprite()
    {
        return Resources.Load<Sprite>("Rewards/GoldIcon");
    }

    public override string GetLabel()
    {
        return $"{gold} 골드";
    }

    public override void Execute()
    {
        DataManager.Inst.playerData.gold += gold;
    }
}