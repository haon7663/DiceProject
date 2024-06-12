using System;
using System.Collections.Generic;
using UnityEngine;

public enum EventEffectType { Hp, Dice, Card, }
public enum CardEventType { Add, Remove, Upgrade }
public enum CompareType { More, Less, Same }

[CreateAssetMenu(fileName = "EventStageSO", menuName = "Scriptable Object/EventStageSO")]
public class EventStageSO : ScriptableObject
{
    [TextArea] public string story;
    public List<EventOption> eventOptions;
}

[Serializable]
public class EventOption
{
    [TextArea] public string description;
    public List<EventEffect> eventEffects;
}

[Serializable]
public class EventEffect
{
    [Header("조건")]
    public bool useCondition;

    [DrawIf("useCondition", true)] public int compareValue;
    [DrawIf("useCondition", true)] public CompareType compareType;
    
    [Header("효과")]
    public EventEffectType eventEffectType;
    [DrawIf("eventEffectType", EventEffectType.Hp)] public int value;
    [DrawIf("eventEffectType", EventEffectType.Dice)] public DiceType diceType;
    [DrawIf("eventEffectType", EventEffectType.Dice)] public bool isAdd;
    [DrawIf("eventEffectType", EventEffectType.Card)] public CardSO cardSO;
    [DrawIf("eventEffectType", EventEffectType.Card)] public CardEventType cardEventType;
}