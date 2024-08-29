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
    public Dictionary<StatType, UnitStat> Stats = new();
    
    private void Start()
    {
        Init();
    }
    
    private void Init()
    {
        Stats = new Dictionary<StatType, UnitStat>
        {
            { StatType.MaxHealth, new UnitStat() },
            { StatType.Cost, new UnitStat() },
            { StatType.GetDamage, new UnitStat() },
            { StatType.TakeDamage, new UnitStat() },
            { StatType.TakeDefence, new UnitStat() },
            { StatType.TakeRecovery, new UnitStat() },
        };
    }
}