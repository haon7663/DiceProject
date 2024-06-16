using System.Collections;
using System.Collections.Generic;
using GDX.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonDontDestroyOnLoad<DataManager>
{
    [Header("주사위")]
    public SerializableDictionary<DiceType, int> diceCount;
    
    [Header("체력")]
    public int maxHp;
    public int curHp;

    [Header("카드")]
    public List<CardSO> cardSOs;

    [Header("유물")]
    public List<RelicSO> relicSOs;

    private void Awake()
    {
        diceCount = new SerializableDictionary<DiceType, int>()
        {
            { DiceType.Four, 9999999 },
            { DiceType.Six, 12 },
            { DiceType.Eight, 6 },
            { DiceType.Twelve, 2 },
            { DiceType.Twenty, 1 },
        };
    }
}
