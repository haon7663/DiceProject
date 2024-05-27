using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        var diceObject = fourDice;
        switch (diceType)
        {
            case DiceType.Four:
                diceObject = fourDice;
                break;
            case DiceType.Six:
                diceObject = sixDice;
                break;
            case DiceType.Eight:
                diceObject = eightDice;
                break;
            case DiceType.Twelve:
                diceObject = twelveDice;
                break;
            case DiceType.Twenty:
                diceObject = twentyDice;
                break;
            default:
                return;
        }
        diceObject.SetActive(true);
        valueTMP.text = value.ToString();
    }
} 
