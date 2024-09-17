using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public event Action<int> Use;
    
    public Panel panel;
    public ItemSO Data { get; private set; }

    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text description;

    [SerializeField] private Unit player;
    [SerializeField] private Unit enemy;

    private int _index;
    
    public void Initialize(ItemSO itemSO, int index)
    {
        Data = itemSO;
        
        icon.sprite = itemSO.sprite;
        nameLabel.text = itemSO.itemName;
        description.text = itemSO.description;

        _index = index;
    }

    public void UseItem()
    {
        if (!Data) return;

        Use?.Invoke(_index);
        
        var behaviours = Data.behaviourInfos.CreateBehaviours();

        foreach (var behaviour in behaviours)
        {
            var target = behaviour.onSelf ? player : enemy;
            
            print(target.name);
            
            if (behaviour.GetType() == BehaviourType.Attack.GetBehaviourClass())
            {
                if (target.TryGetComponent<Health>(out var health))
                {
                    print(target.name);
                    health.OnDamage(behaviour.value);
                }
            }
            
            if (behaviour.GetType() == BehaviourType.StatusEffect.GetBehaviourClass())
            {
                if (behaviour is StatusEffectBehaviour statusEffectBehaviour)
                {
                    if (target.TryGetComponent<StatusEffect>(out var statusEffect))
                    {
                        print(target.name);
                        statusEffect.AddEffect(statusEffectBehaviour.statusEffectSO, behaviour.value);
                    }
                }
            }
        }
    }
}