using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelController : MonoBehaviour
{
    [SerializeField] private ItemPanelController itemPanelController;
    
    [SerializeField] private Transform parent;
    private InventoryFrame[] _inventoryFrames = new InventoryFrame[8];

    private int _currentIndex;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => DataManager.Inst.playerData != null);
        
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
            var itemName = DataManager.Inst.playerData.Items[i];
            if (itemName == "") continue;
            var item = itemName.ToItem();
            if (!item) continue;
            _inventoryFrames[i].Initialize(item);
            
            var index = i;
            _inventoryFrames[i].button.onClick.AddListener(() => SelectItem(item, index));
            _inventoryFrames[i].button.onClick.AddListener(() => SoundManager.Inst.Play("Click_S"));
        }
    }

    public void RemoveItem(int index)
    {
        if (_inventoryFrames[index])
        {
            _inventoryFrames[index].Remove();
            DataManager.Inst.playerData.Items[index] = "";
        }
    }

    public void SelectItem(ItemSO itemSO, int index)
    {
        if (_currentIndex == index)
        {
            itemPanelController.Hide();
            _currentIndex = -1;
            return;
        }
        itemPanelController.Show(itemSO, index);
        _currentIndex = index;
    }
}
