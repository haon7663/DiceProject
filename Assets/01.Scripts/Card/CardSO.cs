using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum CardType { Attack, Defence, Both }

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Object/Card")]
public class CardSO : ScriptableObject
{
    [Header("정보")]
    public Sprite sprite;
    public string cardName;
    [TextArea] public string description;
    public CardType type;
    
    [Header("능력치")]
    public List<BehaviourInfo> behaviourInfos;
}