using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Map;
using UnityEngine;
using UnityEngine.Serialization;

public enum StatType { MaxHealth = 100, Cost = 200, GetDamage = 300, TakeDamage = 400, TakeDefence = 500, TakeRecovery = 600 }

public class Unit : MonoBehaviour
{
    public UnitType type;
    public UnitSO unitSO;
    public Dictionary<StatType, CreatureStat> Stats = new();
    
    private void Start()
    {
        Init();
    }
    
    private void Init()
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
}