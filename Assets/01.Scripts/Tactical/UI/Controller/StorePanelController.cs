using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePanelController : MonoBehaviour
{
    public Panel panel;
    
    [SerializeField] private StorePanel storePanelPrefab;
    [SerializeField] private Transform parent;

    public void GeneratePanels(List<ItemSO> items, List<RelicSO> relics)
    {
        panel.SetPosition(PanelStates.Show, true);
        foreach (var item in items)
        {
            var storePanel = Instantiate(storePanelPrefab, parent);
            storePanel.Initialize(item);
            storePanel.BuyItem += BuyItem;
        }
        foreach (var relic in relics)
        {
            var storePanel = Instantiate(storePanelPrefab, parent);
            storePanel.Initialize(relic);
            storePanel.BuyRelic += BuyRelic;
        }
    }

    public void BuyItem(StorePanel storePanel, ItemSO data, int price)
    {
        if (DataManager.Inst.playerData.Gold >= price)
        {
            for (var i = 0; i < 8; i++)
            {
                if (DataManager.Inst.playerData.Items[i] != null) continue;
                DataManager.Inst.playerData.Items[i] = data.ToJson();
                break;
            }

            DataManager.Inst.playerData.Gold -= price;
            storePanel.Purchased();
        }
        else
        {
            //골드 부족
        }
    }
    
    public void BuyRelic(StorePanel storePanel, RelicSO data, int price)
    {
        if (DataManager.Inst.playerData.Gold >= price)
        {
            DataManager.Inst.playerData.Relics.Add(data.ToJson());
            
            DataManager.Inst.playerData.Gold -= price;
            storePanel.Purchased();
        }
        else
        {
            //골드 부족
        }
    }
}
