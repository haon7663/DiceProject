using System;
using System.Collections.Generic;
using System.Linq;
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
    public int animationCount;
    
    [Header("능력치")]
    public List<BehaviourInfo> behaviourInfos;
    
    [Header("사용 조건")]
    public List<SkillCondition> skillConditions;

    public bool IsSatisfied(Unit unit)
    {
        return skillConditions.Count == 0 || skillConditions.All(s => s.IsSatisfied(unit));
    }
}