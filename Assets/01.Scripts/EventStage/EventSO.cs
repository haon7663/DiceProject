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

[Serializable]
public class EventChoice
{
    public string title;
    [TextArea]
    public string description;
    
    public List<DiceType> diceTypes;
    public List<GameActionWithCompare> actions;
    
    public void ExecuteActions()
    {
        foreach (var action in actions)
        {
            if (action.compareInfo.IsSatisfied(diceTypes.Sum(d => d.GetDiceValue())))
            {
                action.Value.Execute();
            }
        }
    }
}