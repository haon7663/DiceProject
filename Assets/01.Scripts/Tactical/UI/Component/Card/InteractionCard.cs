using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionCard : Card, IPointerClickHandler
{
    public event Action<CardSO, bool> Copy;
    public event Action<bool> Prepare;
    public event Action<InteractionCard> Interact;

    [SerializeField] private GameObject[] useTriggers;

    public bool interactable;
    private bool _isPrepared;

    public void OnCancel()
    {
        panel.SetPosition(PanelStates.Show, true);
        _isPrepared = false;
        SetUseTriggers(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return;

        Interact?.Invoke(this);
        if (_isPrepared)
        {
            Prepare?.Invoke(true);
            panel.SetPosition("Use", true, 0.5f);
            _isPrepared = false;
            SetUseTriggers(false);
        }
        else
        {
            Copy?.Invoke(Data, true);
            panel.SetPosition("Ready", true);
            _isPrepared = true;
            SetUseTriggers(true);
        }
    }

    private void SetUseTriggers(bool active)
    {
        foreach (var useTrigger in useTriggers)
        {
            useTrigger.gameObject.SetActive(active);
        }
    }
}
