using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Object/EnemyData")]
public class EnemyData : CreatureData
{
    [Header("방어 스탯")]
    public List<DiceType> defDiceTypes;
    public int basicDef;
    
    [Header("반격 스탯")]
    public List<DiceType> counterDiceTypes;
    public int basicCounter;
    
    [Header("회피 스탯")]
    public List<DiceType> dodgeDiceTypes;
    public int basicDodge;
}
