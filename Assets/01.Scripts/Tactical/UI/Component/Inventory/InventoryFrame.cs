using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryFrame : MonoBehaviour
{
    public ItemSO Data { get; private set; }

    public Button button;
    [SerializeField] private Image icon;

    public void Initialize(ItemSO itemSO)
    {
        Data = itemSO;
        icon.sprite = itemSO.sprite;
        icon.gameObject.SetActive(true);
    }

    public void Remove()
    {
        Data = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
    }
}
