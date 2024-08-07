using System;
using System.Collections;
using System.Collections.Generic;
using GDX.Collections.Generic;
using UnityEngine;

public class DiceCountPanelController : MonoBehaviour
{
    [SerializeField] private SerializableDictionary<DiceType, DiceCountPanel> dicePanels;

    private void Start()
    {
        SetCount();
    }

    public void SetCount()
    {
        foreach (var dice in DataManager.Inst.PlayerData.dices)
        {
            if (dice.Key == DiceType.Four)
                continue;
            
            dicePanels[dice.Key].SetLabel(dice.Value);
        }
    }
}
