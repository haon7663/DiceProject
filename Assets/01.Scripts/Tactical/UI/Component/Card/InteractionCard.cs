using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionCard : Card, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public event Action<CardSO, bool> Copy;
    public event Action<bool> Prepare;
    public event Action<InteractionCard> Interact;
    
    private bool _isPrepared;
    private Vector2 _startPos;
    private Vector2 _endPos;
    
    public void OnCancel()
    {
        panel.SetPosition(PanelStates.Show, true);
        _isPrepared = false;
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
        Interact?.Invoke(this);
        if (_isPrepared)
        {
            Prepare?.Invoke(true);
            panel.SetPosition("Use", true, 0.5f);
            _isPrepared = false;
        }
        else
        {
            Copy?.Invoke(Data, true);
            panel.SetPosition("Ready", true);
            _isPrepared = true;
        }
    }
}
