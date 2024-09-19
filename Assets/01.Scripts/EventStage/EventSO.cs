using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EventData", menuName = "EventSystem/Event")]
public class EventData : ScriptableObject
{
    public string eventTitle;
    public Sprite eventSprite;
    [TextArea]
    public string eventDescription;
    public List<EventChoice> choices;
}

public enum EventConditionType
{
    None,
    Item,
    Dice,
    Gold
}

[Serializable]
public class EventChoice
{
    public string title;
    [TextArea]
    public string description;

    public EventConditionType eventConditionType;
    
    [DrawIf("eventConditionType", EventConditionType.Dice)]
    public List<DiceType> needDices;
    [DrawIf("eventConditionType", EventConditionType.Item)]
    public ItemSO needItem;
    [DrawIf("eventConditionType", EventConditionType.Gold)]
    public int needGold;
    
    public List<GameActionWithCompare> actions;
    
    public void ExecuteActions(int value)
    {
        foreach (var action in actions)
        {
            if (action.compareInfo.IsSatisfied(value))
            {
                action.Value.Execute();
            }
        }
    }
}