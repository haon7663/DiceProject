using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPanelController : MonoBehaviour
{
    [SerializeField] private Panel panel;
    [SerializeField] private Panel disablePanel;

    private void Start()
    {
        panel.SetPosition(PanelStates.Show);
    }

    public void Show()
    {
        panel.SetPosition(PanelStates.Show, true, 0.5f);
    }
    
    public void Hide()
    {
        panel.SetPosition(PanelStates.Hide, true, 1f);
    }
    
    public void Enable()
    {
        panel.SetPosition("Enable", true, 0.5f);
        disablePanel.SetPosition(PanelStates.Hide, true, 0.5f);
    }

    public void Disable()
    {
        panel.SetPosition("Disable", true, 0.5f);
        disablePanel.SetPosition(PanelStates.Show, true, 0.5f);
    }
}
