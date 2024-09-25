using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionCard : Card, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public event Action<CardSO, bool> Copy;
    public event Action<bool> Prepare;
    public event Action<InteractionCard> Interact;

    [SerializeField] private GameObject[] useTriggers;

    public bool interactable;
    private bool _isPrepared;

    private Vector2 _startPos;
    private Vector2 _endPos;

    public void OnCancel()
    {
        panel.SetPosition(PanelStates.Show, true);
        _isPrepared = false;
        SetUseTriggers(false);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _startPos = transform.parent.position;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        _endPos = transform.parent.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return;
        if ((_startPos - _endPos).sqrMagnitude > 0.1f) return;

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
