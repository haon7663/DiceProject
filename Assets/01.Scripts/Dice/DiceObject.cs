using System;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DiceObject : MonoBehaviour
{
    [SerializeField] private GameObject fourDice;
    [SerializeField] private GameObject sixDice;
    [SerializeField] private GameObject eightDice;
    [SerializeField] private GameObject twelveDice;
    [SerializeField] private GameObject twentyDice;
    [SerializeField] private TMP_Text valueTMP;

    public void SetUp(DiceType diceType, int value) 
    {
        switch (diceType)
        {
            case DiceType.Four:
                fourDice.SetActive(true);
                break;
            case DiceType.Six:
                sixDice.SetActive(true);
                break;
            case DiceType.Eight:
                eightDice.SetActive(true);
                break;
            case DiceType.Twelve:
                twelveDice.SetActive(true);
                break;
            case DiceType.Twenty:
                twentyDice.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(diceType), diceType, null);
        }
        valueTMP.text = value.ToString();
    }
}
