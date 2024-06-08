using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public enum StatType { MaxHealth = 100, Cost = 200, GetDamage = 300, TakeDamage = 400, TakeDefence = 500, TakeRecovery = 600 }

public class Creature : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetUp();
    }

    public CreatureSO creatureSO;
    public Dictionary<StatType, CreatureStat> Stats = new();
    
    private Creature _targetCreature;
    public CardSO CardSO { get; private set; }
    
    [Header("스탯")]
    public float maxHp;
    public float curHp;
    public float defence;
    
    private void SetUp()
    {
        Stats = new Dictionary<StatType, CreatureStat>
        {
            { StatType.MaxHealth, new CreatureStat() },
            { StatType.Cost, new CreatureStat() },
            { StatType.GetDamage, new CreatureStat() },
            { StatType.TakeDamage, new CreatureStat() },
            { StatType.TakeDefence, new CreatureStat() },
            { StatType.TakeRecovery, new CreatureStat() },
        };
        
        maxHp = Stats[StatType.MaxHealth].GetValue(creatureSO.hp);
        curHp = maxHp;
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    public void SetCard(CardSO cardSO)
    {
        CardSO = cardSO;
    }
    
    public void OnDamage(int damage)
    {
        curHp -= damage;
        UIManager.inst.PopDamageText(transform.position, damage);
        UIManager.inst.SetHealth(curHp, maxHp, creatureSO.creatureType == CreatureType.Player);
    }

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }
}