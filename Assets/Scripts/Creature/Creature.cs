using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Map;
using UnityEngine;

public enum StatType { MaxHealth = 100, Cost = 200, GetDamage = 300, TakeDamage = 400, TakeDefence = 500, TakeRecovery = 600 }

public class Creature : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetUp();
    }

    public CreatureType creatureType;
    public CreatureSO creatureSO;
    public Dictionary<StatType, CreatureStat> Stats = new();
    
    private Creature _targetCreature;
    public CardSO CardSO { get; private set; }
    
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
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    public void SetAlpha(float alpha)
    {
        _spriteRenderer.color = new Color(1, 1, 1, alpha);
    }

    public void SetCard(CardSO cardSO)
    {
        CardSO = cardSO;
    }

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }
}