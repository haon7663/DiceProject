using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EventOptionController : MonoBehaviour
{
    [SerializeField] private GameObject eventOptionPrefab;
    [SerializeField] private Transform parent;

    public void ShowEvent(EventSO eventSO)
    {
        foreach (var eventOption in eventSO.eventOptions)
        {
            var button = Instantiate(eventOptionPrefab, parent).GetComponent<EventOptionButton>();
            button.Init(eventOption);
        }
    }
}
