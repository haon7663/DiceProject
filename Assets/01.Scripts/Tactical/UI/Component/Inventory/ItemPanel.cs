using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public event Action<int> Use;
    
    public Panel panel;
    public ItemSO Data { get; private set; }
    public RelicSO RelicData { get; private set; }

    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Button useButton;

    [SerializeField] private GameObject priceObject;
    [SerializeField] private TMP_Text priceLabel;
    [SerializeField] private Button buyButton;

    private int _index;
    
    public void Initialize(ItemSO itemSO, int index)
    {
        Data = itemSO;
        
        icon.sprite = itemSO.sprite;
        nameLabel.text = itemSO.itemName;
        description.text = itemSO.description;

        _index = index;
        
        priceObject.SetActive(false);
        useButton.gameObject.SetActive(true);
        buyButton.gameObject.SetActive(false);
    }
    
    public void Initialize(RelicSO relicSO)
    {
        RelicData = relicSO;
        
        icon.sprite = relicSO.sprite;
        nameLabel.text = relicSO.name;
        description.text = relicSO.description;
        
        priceObject.SetActive(false);
        useButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
    }
    
    public void InitializeToBuy(StorePanel storePanel, ItemSO data, int price)
    {
        Data = data;
        
        icon.sprite = data.sprite;
        nameLabel.text = data.itemName;
        description.text = data.description;

        priceObject.SetActive(true);
        priceLabel.text = price.ToString();
        
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => BuyItem(storePanel, data, price));
        
        panel.SetPosition(PanelStates.Show, true, 0.25f);
        useButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(true);
    }
    
    public void InitializeToBuy(StorePanel storePanel, RelicSO data, int price)
    {
        RelicData = data;
        
        icon.sprite = data.sprite;
        nameLabel.text = data.name;
        description.text = data.description;

        priceObject.SetActive(true);
        priceLabel.text = price.ToString();
        
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => BuyRelic(storePanel, data, price));
        
        panel.SetPosition(PanelStates.Show, true, 0.25f);
        useButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(true);
    }

    public void BuyItem(StorePanel storePanel, ItemSO data, int price)
    {
        if (DataManager.Inst.playerData.Gold >= price)
        {
            for (var i = 0; i < 8; i++)
            {
                if (!string.IsNullOrEmpty(DataManager.Inst.playerData.Items[i])) continue;
                DataManager.Inst.playerData.Items[i] = data.ToJson();
                break;
            }

            DataManager.Inst.playerData.Gold -= price;
            storePanel.Purchased();
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
    }

    public void UseItem()
    {
        if (!Data) return;

        Use?.Invoke(_index);
        
        var behaviours = Data.behaviourInfos.CreateBehaviours();

        foreach (var behaviour in behaviours)
        {
            var target = behaviour.onSelf ? BattleController.Inst.player : BattleController.Inst.enemy;
            
            if (behaviour.GetType() == BehaviourType.Attack.GetBehaviourClass())
            {
                if (target.TryGetComponent<Health>(out var health))
                {
                    health.OnDamage(behaviour.value);
                }
            }
            
            if (behaviour.GetType() == BehaviourType.StatusEffect.GetBehaviourClass())
            {
                if (behaviour is StatusEffectBehaviour statusEffectBehaviour)
                {
                    if (target.TryGetComponent<StatusEffect>(out var statusEffect))
                    {
                        statusEffect.AddEffect(statusEffectBehaviour.statusEffectSO, behaviour.value);
                    }
                }
            }
        }

        Data.ExecuteActions();
    }
}