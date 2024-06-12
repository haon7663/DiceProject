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

    private void Awake()
    {
        diceCount = new SerializableDictionary<DiceType, int>()
        {
            { DiceType.Four, 9999999 },
            { DiceType.Six, 12 },
            { DiceType.Eight, 9 },
            { DiceType.Twelve, 4 },
            { DiceType.Twenty, 2 },
        };
    }
}
