using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{
    [Header("정보")]
    public Sprite sprite;
    public string cardName;
    [TextArea] public string description;
    public CardType type;
    
    [Header("능력치")]
    public List<CardEffect> cardEffects;
}

public enum CardType { Attack, Defence }
public enum CardBehaviorType { Damage, StatusEffect, Defence, Avoid, Counter }

[Serializable]
public class CardEffect
{
    public bool onSelf;
    public CardBehaviorType cardBehaviorType;
    [DrawIf("cardBehaviorType", CardBehaviorType.StatusEffect)]
    public StatusEffectSO statusEffectSO;
    
    [Header("스탯")]
    public int basicValue;
    public List<DiceType> diceTypes;
}