using System;
using System.Collections.Generic;

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
    
    /*public static List<Behaviour> CreateRewards(this List<RewardInfo> rewardInfos)
    {
        var rewards = new List<Reward>();
        foreach (var rewardInfo in rewardInfos)
        {
            var reward = (Reward)Activator.CreateInstance(rewardInfo.rewardType.GetRewardClass());
            if (reward is CardReward cardReward)
            {
                statusEffectBehaviour.statusEffectSO = behaviourInfo.statusEffectSO;
            }
            behaviours.Add(behaviour);
        }
        
        return behaviours;
    }*/
}