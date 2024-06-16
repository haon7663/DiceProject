using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventOptionButton : MonoBehaviour
{
    [SerializeField] private TMP_Text descriptionTMP;

    private EventOption _eventOption;
    public void SetUp(EventOption eventOption)
    {
        _eventOption = eventOption;
        descriptionTMP.text = eventOption.description;
    }

    public void InvokeEvent()
    {
        EventStageManager.Inst.InvokeEvent(_eventOption);
    }
}
