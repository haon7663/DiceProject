using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionCard : Card
{
    public event Action<CardSO, bool> Copy;
    public event Action<bool> Prepare;
    
    private bool _isPrepared;
    
    public void OnPointerClick()
    {
        if (_isPrepared)
        {
            Prepare?.Invoke(true);
            panel.SetPosition(PanelStates.Show, true);
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
