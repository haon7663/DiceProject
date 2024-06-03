using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{
    [Header("정보")]
    public Sprite sprite;
    public string cardName;
    [TextArea] public string description;
    public CardType cardType;

    [Header("능력치")]
    public List<CardData> cardData;
}

public enum CardType { Attack, Defence }
public enum BehaviorType { Damage, StatusEffect }
public enum CompareType { More, Less, Same }

[Serializable]
public class CardData
{
    public bool onSelf;
    public BehaviorType behaviorType;
    [DrawIf("behaviorType", BehaviorType.StatusEffect)] public StatusEffectSO statusEffectSO;
    
    [Header("스탯")]
    public int basicValue;
    public List<DiceType> diceTypes;
    
    [Header("조건")] 
    public bool useCondition;

    [DrawIf("useCondition", true)] public int standardValue;
    [DrawIf("useCondition", true)] public CompareType compareType;

    public void UseCard()
    {
        foreach (var diceType in diceTypes)
        {
            
        }
    }

    public int GetDiceValue(DiceType diceType)
    {
        var maxSize = diceType switch
        {
            DiceType.Four => 4,
            DiceType.Six => 6,
            DiceType.Eight => 8,
            DiceType.Twelve => 12,
            DiceType.Twenty => 20,
            _ => 0
        };
        return Random.Range(1, maxSize + 1);;
    }
}