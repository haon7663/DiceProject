using System.Collections.Generic;
using UnityEngine;

public enum UnitType { Player, Enemy }

[CreateAssetMenu(menuName = "Scriptable Object/UnitSO", fileName = "UnitSO")]
public class UnitSO : ScriptableObject
{
    public new string name;
    
    [Header("스프라이트")]
    public Sprite idleSprite;
    public List<AnimationData> attacks;
    public List<AnimationData> hits;

    [Header("카드")] 
    public List<CardSO> cards;

    [Header("스탯")] 
    public int maxHp;
}
