using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventSO", menuName = "Scriptable Object/EventSO")]
public class EventSO : ScriptableObject
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
    public List<DiceType> diceTypes;
    public List<EventEffect> eventEffects;
}

public enum EventEffectType { Hp, Dice, Card, Relic, None }
public enum CardEventType { Add, Remove, Upgrade }

[Serializable]
public class EventEffect
{
    [Header("로그")]
    [TextArea] public string eventLog;
    
    [Header("비교")]
    public CompareInfo compareInfo;
    
    [Header("효과")]
    public EventEffectType eventEffectType;
    [DrawIf("eventEffectType", EventEffectType.Hp)] public int value;
    [DrawIf("eventEffectType", EventEffectType.Dice)] public DiceType diceType;
    [DrawIf("eventEffectType", EventEffectType.Dice)] public bool isAdd;
    [DrawIf("eventEffectType", EventEffectType.Card)] public CardSO cardSO;
    [DrawIf("eventEffectType", EventEffectType.Card)] public CardEventType cardEventType;
    [DrawIf("eventEffectType", EventEffectType.Relic)] public RelicSO relicSO;
}