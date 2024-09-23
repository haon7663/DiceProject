using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EventChoicesController : MonoBehaviour
{
    public event Action<EventChoice> OnExecute;

    [SerializeField] private Panel panel;
    [SerializeField] private EventChoiceButton eventChoiceButtonPrefab;
    [SerializeField] private Transform parent;

    public void ShowEventChoices(EventData eventData)
    {
        panel.SetPosition(PanelStates.Show, true, 0.5f);
        
        var choices = eventData.choices;
        foreach (var choice in choices)
        {
            var eventChoice = Instantiate(eventChoiceButtonPrefab, parent);
            eventChoice.Initialize(choice);
            eventChoice.OnExecute += Execute;
        }
    }

    private void Execute(EventChoice eventChoice)
    {
        OnExecute?.Invoke(eventChoice);
    }
}
