using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum UnitType { Player, Enemy }

[CreateAssetMenu(menuName = "Scriptable Object/UnitSO", fileName = "UnitSO")]
public class UnitSO : ScriptableObject
{
    public new string name;
    
    [Header("스프라이트")]
    public Sprite idleSprite;
    public Sprite attackSprite;
    public Sprite attackEffectSprite;
    public Sprite defenceSprite;
    public Sprite defenceEffectSprite;
    public Sprite avoidSprite;

    [Header("카드")] 
    public List<CardSO> cards;

    [Header("스탯")] 
    public int maxHp;
}