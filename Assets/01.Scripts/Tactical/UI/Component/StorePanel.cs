using System;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum StoreItemType
{
    None,
    Item,
    Relic
}

public class StorePanel : MonoBehaviour
{
    public event Action<StorePanel, ItemSO, int> BuyItem;
    public event Action<StorePanel, RelicSO, int> BuyRelic;
    
    public StoreItemType storeItemType;
    [DrawIf("storeItemType", StoreItemType.Item)]
    public ItemSO itemSO;
    [DrawIf("storeItemType", StoreItemType.Relic)]
    public RelicSO relicSO;

    [Header("UI")]
    public Image icon;
    public TMP_Text priceLabel;
    public GameObject purchasedPanel;

    private Button _button;
    private int _price;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void Initialize(ItemSO data)
    {
        storeItemType = StoreItemType.Item;
        itemSO = data;

        _price = Random.Range(30, 46);

        icon.sprite = itemSO.sprite;
        priceLabel.text = _price.ToString();
    }
    
    public void Initialize(RelicSO data)
    {
        storeItemType = StoreItemType.Relic;
        relicSO = data;
        
        _price = Random.Range(150, 226);

        icon.sprite = relicSO.sprite;
        priceLabel.text = _price.ToString();
    }

    public void BuyActionHandler()
    {
        switch (storeItemType)
        {
            case StoreItemType.Item:
                BuyItem?.Invoke(this, itemSO, _price);
                break;
            case StoreItemType.Relic:
                BuyRelic?.Invoke(this, relicSO, _price);
                break;
        }
    }

    public void Purchased()
    {
        _button.interactable = false;
        purchasedPanel.SetActive(true);
        priceLabel.text = "구매됨";
    }
}