using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChangeController : MonoBehaviour
{
    [SerializeField] private PanelChangeButton cardButton;
    [SerializeField] private PanelChangeButton inventoryButton;
    [SerializeField] private PanelChangeButton relicButton;

    private void Start()
    {
        Select("card");
    }

    public void Select(string panelName)
    {
        cardButton.DeActive();
        inventoryButton.DeActive();
        relicButton.DeActive();
        
        switch (panelName)
        {
            case "card":
                cardButton.Active();
                break;
            case "inventory":
                inventoryButton.Active();
                break;
            case "relic":
                relicButton.Active();
                break;
        }
    }
}
