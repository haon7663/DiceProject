using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{
    [Header("정보")]
    [JsonIgnore]
    public Sprite sprite;
    public string cardName;
    [TextArea] public string description;
    public CardType cardType;

    [Header("능력치")]
    [JsonIgnore]
    public List<CardData> cardData;
}

public enum CardType { Attack, Defence }
public enum BehaviorType { Damage, StatusEffect, Defence, Avoid, Counter }

[Serializable]
public class CardData
{
    public bool onSelf;
    public BehaviorType behaviorType;
    [DrawIf("behaviorType", BehaviorType.StatusEffect)] public StatusEffectSO statusEffectSO;
    
    [Header("스탯")]
    public int basicValue;
    public List<DiceType> diceTypes;
}