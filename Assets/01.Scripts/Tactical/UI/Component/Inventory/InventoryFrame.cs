using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryFrame : MonoBehaviour
{
    public ItemSO Data { get; private set; }
    
    [SerializeField] private Image icon;

    public void Initialize(ItemSO itemSO)
    {
        Data = itemSO;
        icon.sprite = itemSO.sprite;
    }
}
