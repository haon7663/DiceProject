using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GDX.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceCountPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text countChangePrefab;
    [SerializeField] private SerializableDictionary<DiceType, DiceCountPanel> dicePanels;

    private void Start()
    {
        UpdateCount();
    }

    public void UpdateCount()
    {
        foreach (var dice in DataManager.Inst.playerData.Dices)
        {
            if (dice.Key == DiceType.Four)
                continue;
            
            dicePanels[dice.Key].SetLabel(dice.Value);
        }
    }

    public void AddCount(DiceType diceType, int value)
    {
        if (diceType == DiceType.Four)
            return;
        
        var countChange = Instantiate(countChangePrefab, dicePanels[diceType].transform);
        countChange.text = value > 0 ? $"+{value}" : $"{value}";
        countChange.rectTransform.DOAnchorPosY(countChange.rectTransform.anchoredPosition.y + 75, 0.75f)
            .OnComplete(() => Destroy(countChange.gameObject));

        UpdateCount();
    }
}
