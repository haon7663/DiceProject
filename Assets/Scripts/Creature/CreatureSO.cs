using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum CreatureType { Player, Enemy }

public class CreatureSO : ScriptableObject
{
    [Header("스프라이트")]
    public Sprite idleSprite;
    public Sprite attackSprite;
    public Sprite defenceSprite;
    public Sprite avoidSprite;

    [Header("카드")] 
    public List<CardSO> cards;

    [Header("스탯")] 
    public int hp;
}