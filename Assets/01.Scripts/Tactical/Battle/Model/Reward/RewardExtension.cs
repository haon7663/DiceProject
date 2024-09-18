using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RewardExtension
{
    public static Type GetRewardClass(this RewardType rewardType)
    {
        return rewardType switch
        {
            RewardType.Card => typeof(CardReward),
            RewardType.Dice => typeof(DiceReward),
            RewardType.Gold => typeof(GoldReward),
            RewardType.Item => typeof(ItemReward),
            RewardType.Relic => typeof(RelicReward),
            _ => throw new ArgumentOutOfRangeException(nameof(rewardType), rewardType, null)
        };
    }
    public static List<CardSO> SelectRandomCards(int count)
    {
        var selectedCards = new List<CardSO>();
        var cards = Resources.LoadAll<CardSO>("Cards");
        for (var i = 0; i < count; i++)
        {
            var card = cards.Where(c =>
                DataManager.Inst.playerData.Cards.All(card => card.name != c.cardName) &&
                selectedCards.All(card => card.cardName != c.cardName)).ToList().Random();
            selectedCards.Add(card);
        }
        return selectedCards;
    }
}