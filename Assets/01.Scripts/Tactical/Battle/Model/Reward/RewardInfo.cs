using System;
using OneLine;

[Serializable]
public class RewardInfo
{
    public RewardType rewardType;
    [OneLineWithHeader]
    public IntMinMax count;
    
    public bool isRandom;

    [DrawIf("rewardType", RewardType.Card)] [DrawIf("isRandom", false)]
    public CardSO cardSO;
    
    [DrawIf("rewardType", RewardType.Dice)] [DrawIf("isRandom", false)]
    public DiceType diceType;
    
    [DrawIf("rewardType", RewardType.Item)] [DrawIf("isRandom", false)]
    public ItemSO itemSO;
    
    [DrawIf("rewardType", RewardType.Relic)] [DrawIf("isRandom", false)]
    public RelicSO relicSO;
}