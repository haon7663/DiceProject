using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanelController : MonoBehaviour
{
    [SerializeField] private ItemPanel itemPanel;

    public void Show(ItemSO itemSO)
    {
        itemPanel.Initialize(itemSO);
        itemPanel.panel.SetPosition(PanelStates.Show, true, 0.25f);
    }
    
    public void Hide()
    {
        itemPanel.panel.SetPosition(PanelStates.Hide, true, 0.25f);
    }
}
