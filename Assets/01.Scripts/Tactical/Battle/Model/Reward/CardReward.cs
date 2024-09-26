using System.Collections.Generic;
using UnityEngine;

public class CardReward : Reward
{
    private List<CardSO> cards;
    
    public CardReward(List<CardSO> cards)
    {
        this.cards = cards;
    }
    
    public override Sprite GetSprite()
    {
        return Resources.Load<Sprite>("Rewords/CardIcon");
    }

    public override string GetLabel()
    {
        return "덱에 카드를 추가";
    }

    public override void Execute()
    {
        if (cards.Count <= 0) return;
        RewardCardController.Inst.ShowCards(cards);
    }
}