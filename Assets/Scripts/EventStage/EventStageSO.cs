using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventEffectType { Hp, Dice, Card, Relic, None }
public enum CardEventType { Add, Remove, Upgrade }
public enum CompareType { More, Less, Same }

[CreateAssetMenu(fileName = "EventStageSO", menuName = "Scriptable Object/EventStageSO")]
public class EventStageSO : ScriptableObject
{
    public Sprite eventSprite;
    [TextArea] public string story;
    public List<EventOption> eventOptions;
}

[Serializable]
public class EventOption
{
    [TextArea] public string description;
    
    [Header("조건")]
    public bool useCondition;
    public List<DiceType> compareDiceTypes;
    
    public List<EventEffect> eventEffects;
}

[Serializable]
public class EventEffect
{
    [Header("비교")]
    public CompareType compareType;
    public int compareValue;
    
    [Header("효과")]
    public EventEffectType eventEffectType;
    [DrawIf("eventEffectType", EventEffectType.Hp)] public int value;
    [DrawIf("eventEffectType", EventEffectType.Dice)] public DiceType diceType;
    [DrawIf("eventEffectType", EventEffectType.Dice)] public bool isAdd;
    [DrawIf("eventEffectType", EventEffectType.Card)] public CardSO cardSO;
    [DrawIf("eventEffectType", EventEffectType.Card)] public CardEventType cardEventType;
    [DrawIf("eventEffectType", EventEffectType.Relic)] public RelicSO relicSO;

    [Header("로그")]
    [TextArea] public string eventLog;
}