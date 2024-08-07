using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOptionController : MonoBehaviour
{
    [SerializeField] private Transform layoutGroup;
    [SerializeField] private GameObject eventButtonPrefab;

    public void ShowEvent(EventSO eventSO)
    {
        foreach (var eventOption in eventSO.eventOptions)
        {
            var eventOptionButton = Instantiate(eventButtonPrefab, layoutGroup).GetComponent<EventOptionButton>();
            eventOptionButton.Init(eventOption);
        }
    }
}
