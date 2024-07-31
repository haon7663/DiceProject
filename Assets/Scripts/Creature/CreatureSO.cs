using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

public enum CreatureType { Player, Enemy }

public class CreatureSO : ScriptableObject
{
    [Header("스프라이트")]
    [JsonIgnore]
    public Sprite idleSprite;
    [JsonIgnore]
    public Sprite attackSprite;
    [JsonIgnore]
    public Sprite defenceSprite;
    [JsonIgnore]
    public Sprite avoidSprite;

    [Header("카드")] 
    [JsonIgnore]
    public List<CardSO> cards;

    [Header("스탯")] 
    public int hp;
}
