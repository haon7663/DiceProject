using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventChoiceButton : MonoBehaviour
{
    public event Action<EventChoice> OnExecute;

    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    
    private EventChoice _eventChoice;
    
    public void Initialize(EventChoice eventChoice)
    {
        _eventChoice = eventChoice;
        title.text = eventChoice.title;
        description.text = eventChoice.description;
    }

    public void Execute()
    {
        OnExecute?.Invoke(_eventChoice);
    }
}
