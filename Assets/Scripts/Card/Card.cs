using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Object/CardSO")]
public class Card : ScriptableObject
{
    [Header("정보")]
    public Sprite sprite;
    public string cardName;
    public string description;
    public bool isAttack;

    [Header("능력치")]
    public Dices dices;
}
