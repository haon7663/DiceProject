using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Attack }

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Object/CardData")]
public class CardData : ScriptableObject
{
    public Sprite sprite;
    public string cardName;
    public string description;
    
    public List<DiceType> diceTypes;
    public int basicValue;
}
