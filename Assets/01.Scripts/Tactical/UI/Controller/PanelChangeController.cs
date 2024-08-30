using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChangeController : MonoBehaviour
{
    [SerializeField] private PanelChangeButton cardButton;
    [SerializeField] private Panel cardContentPanel;
    [SerializeField] private PanelChangeButton inventoryButton;
    [SerializeField] private Panel inventoryContentPanel;
    [SerializeField] private PanelChangeButton relicButton;
    [SerializeField] private Panel relicContentPanel;

    private void Start()
    {
        Select("card");
    }

    public void Select(string panelName)
    {
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
}