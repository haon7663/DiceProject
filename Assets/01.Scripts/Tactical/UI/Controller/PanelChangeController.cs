using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PanelChangeController : MonoBehaviour
{
    [SerializeField] private PanelChangeButton cardButton;
    [SerializeField] private Panel cardContentPanel;
    [SerializeField] private PanelChangeButton inventoryButton;
    [SerializeField] private Panel inventoryContentPanel;
    [SerializeField] private PanelChangeButton relicButton;
    [SerializeField] private Panel relicContentPanel;

    private string _currentPanelName;

    private void Start()
    {
        Select("card");
    }

    public void Select(string panelName)
    {
        if (_currentPanelName == panelName) return;
        _currentPanelName = panelName;
        
        cardButton.DeActive();
        inventoryButton.DeActive();
        relicButton.DeActive();
        cardContentPanel.SetPosition(PanelStates.Hide);
        inventoryContentPanel.SetPosition(PanelStates.Hide);
        relicContentPanel.SetPosition(PanelStates.Hide);
        
        switch (panelName)
        {
            case "card":
                cardButton.Active();
                cardContentPanel.SetPosition(PanelStates.Show, true);
                break;
            case "inventory":
                inventoryButton.Active();
                inventoryContentPanel.SetPosition(PanelStates.Show, true);
                break;
            case "relic":
                relicButton.Active();
                relicContentPanel.SetPosition(PanelStates.Show, true);
                break;
        }
    }

    public void ClickSound()
    {
        SoundManager.Inst.Play("Click_L");
    }
}
