using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDialogController : MonoBehaviour
{
    [SerializeField] private Transform layoutGroup;
    [SerializeField] private GameObject eventButtonPrefab;

    public void ShowEvent(EventSO eventSO)
    {
        foreach (var eventOption in eventSO.eventOptions)
        {
            var button = Instantiate(eventButtonPrefab, layoutGroup).GetComponent<EventOptionButton>();
            button.Init(eventOption);
        }
    }
}
