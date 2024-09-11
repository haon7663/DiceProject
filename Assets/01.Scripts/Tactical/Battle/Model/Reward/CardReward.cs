using UnityEngine;

public class CardReward : Reward
{
    public override Sprite GetSprite()
    {
        return Resources.Load<Sprite>("Rewards/CardIcon");
    }

    public override string GetLabel()
    {
        return "덱에 카드를 추가";
    }

    public override void Execute()
    {
        
    }
}