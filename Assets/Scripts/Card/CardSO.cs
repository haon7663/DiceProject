using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{
    [Header("정보")]
    public Sprite sprite;
    public string cardName;
    public string description;
    public CardType cardType;

    [Header("능력치")]
    public List<CardState> cardState;
}

public enum CardType { Attack, Defence }
public enum BehaviorType { Damage, StatusEffect }

[Serializable]
public struct CardState
{
    public BehaviorType behaviorType;
    public bool onSelf;
    
    public List<DiceType> diceTypes;
    public int basicValue;
}