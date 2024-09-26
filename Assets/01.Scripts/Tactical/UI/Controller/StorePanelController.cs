using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePanelController : MonoBehaviour
{
    public Panel panel;
    
    [SerializeField] private StorePanel storePanelPrefab;
    [SerializeField] private Transform parent;

    [SerializeField] private ItemPanel itemPanel;

    public void GeneratePanels(List<ItemSO> items, List<RelicSO> relics)
    {
        panel.SetPosition(PanelStates.Show, true);
        foreach (var item in items)
        {
            var storePanel = Instantiate(storePanelPrefab, parent);
            storePanel.Initialize(item);
            storePanel.BuyItem += itemPanel.InitializeToBuy;
        }
        foreach (var relic in relics)
        {
            var storePanel = Instantiate(storePanelPrefab, parent);
            storePanel.Initialize(relic);
            storePanel.BuyRelic += itemPanel.InitializeToBuy;
        }
    }
}
