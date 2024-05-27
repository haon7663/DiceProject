using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum CreatureType { Player, Enemy }

public class CreatureData : ScriptableObject
{
    public CreatureType creatureType = CreatureType.Enemy;
    
    [Header("스프라이트")]
    public Sprite idleSprite;
    public Sprite attackSprite;

    [Header("카드")] 
    public List<CardData> cardDatas;

    [Header("스탯")] 
    public int hp;
    [Header("방어")] 
    public Dices defenceDices;
    [Header("회피")] 
    public Dices avoidDices;
    [Header("반격")] 
    public Dices counterDices;
}
