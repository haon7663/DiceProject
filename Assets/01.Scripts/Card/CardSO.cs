using System;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Attack, Defence, Both }

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Object/Card")]
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

public enum CardSpecialType { None, Conditional, EqualDice, Reversal }

[Serializable]
public class CardEffect
{
    public bool onSelf;
    public BehaviourType behaviourType;
    public CompareType behaviourCompareType;
    
    [Header("스탯")]
    public int basicValue;
    public List<DiceType> dices;

    [Header("특수")]
    public CardSpecialType specialType;
    [DrawIf("specialType", CardSpecialType.Conditional)]
    public CompareType compareType;
}