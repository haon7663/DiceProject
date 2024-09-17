using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelController : MonoBehaviour
{
    [SerializeField] private ItemPanelController itemPanelController;
    
    [SerializeField] private Transform parent;
    private InventoryFrame[] _inventoryFrames = new InventoryFrame[8];

    private void Start()
    {
        SetInventoryFrames();
        UpdateInventory();
    }

    private void SetInventoryFrames()
    {
        _inventoryFrames = new InventoryFrame[8];
        for (var i = 0; i < 8; i++)
        {
            _inventoryFrames[i] = parent.GetChild(i).GetComponent<InventoryFrame>();
        }
    }

    public void UpdateInventory()
    {
        for (var i = 0; i < 8; i++)
        {
            var itemName = DataManager.Inst.playerData.items[i];
            var item = itemName != "" ? itemName.ToItem() : null;
            if (!item) return;
            _inventoryFrames[i].Initialize(item);
            
            var index = i;
            _inventoryFrames[i].button.onClick.AddListener(() => SelectItem(item, index));
        }
    }

    public void RemoveItem(int index)
    {
        if (_inventoryFrames[index])
        {
            _inventoryFrames[index].Remove();
        }
    }

    public void SelectItem(ItemSO itemSO, int index)
    {
        itemPanelController.Show(itemSO, index);
    }
}
