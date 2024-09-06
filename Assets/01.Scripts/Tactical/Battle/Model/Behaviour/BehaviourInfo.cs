using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class BehaviourInfo
{
    [Header("#BehaviourType")]
    public BehaviourType behaviourType;
    
    [DrawIf("behaviourType", BehaviourType.StatusEffect)]
    public StatusEffectSO statusEffectSO;
    
    [Header("#Value")]
    public int basicValue;
    public List<DiceType> diceTypes;
    
    [Header("#Compare")]
    public CompareInfo compareInfo;
    
    [Header("#Special")]
    public bool onSelf;
    public bool chargeInDice;
}