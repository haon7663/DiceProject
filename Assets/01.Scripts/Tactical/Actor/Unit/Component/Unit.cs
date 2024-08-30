using System.Collections.Generic;
using GDX.Collections.Generic;
using UnityEngine;

public enum StatType { MaxHealth = 100, Cost = 200, GetDamage = 300, TakeDamage = 400, TakeDefence = 500, TakeRecovery = 600 }

public class Unit : MonoBehaviour
{
    public UnitType type;
    public UnitSO unitSO;
    public CardSO cardSO;
    public Dictionary<StatType, UnitStat> Stats = new();

    public Dictionary<CardEffect, int> values;
    
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