using UnityEngine;

public class GoldReward : Reward
{
    public int count;
    
    public GoldReward(int count)
    {
        this.count = count;
    }

    public override Sprite GetSprite()
    {
        return Resources.Load<Sprite>("Rewords/GoldIcon");
    }

    public override string GetLabel()
    {
        return $"{count} 골드";
    }

    public override void Execute()
    {
        DataManager.Inst.playerData.Gold += count;
    }
}