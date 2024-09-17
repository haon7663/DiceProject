using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanelController : MonoBehaviour
{
    [SerializeField] private InventoryPanelController inventoryPanelController;
    [SerializeField] private ItemPanel itemPanel;

    public void Show(ItemSO itemSO, int index)
    {
        itemPanel.Initialize(itemSO, index);
        itemPanel.panel.SetPosition(PanelStates.Show, true, 0.25f);
        itemPanel.Use += inventoryPanelController.RemoveItem;
    }
    
    public void Hide()
    {
        itemPanel.panel.SetPosition(PanelStates.Hide, true, 0.25f);
    }
}
