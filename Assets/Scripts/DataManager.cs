using System.Collections;
using System.Collections.Generic;
using GDX.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public SerializableDictionary<DiceType, int> diceCount;

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
