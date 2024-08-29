using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EventOptionButton : MonoBehaviour
{
    [SerializeField] private TMP_Text descriptionLabel;
    
    private EventOption _eventOption;
    
    public void Init(EventOption eventOption)
    {
        _eventOption = eventOption;
        descriptionLabel.text = eventOption.description;
    }

    public void InvokeEvent()
    {
        EventStageManager.Inst.InvokeEvent(_eventOption);
    }
}
