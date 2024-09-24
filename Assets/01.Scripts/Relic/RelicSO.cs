using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(fileName = "RelicSO", menuName = "Scriptable Object/RelicSO")]
public class RelicSO : ScriptableObject
{
    [Header("정보")]
    public new string name;
    public Sprite sprite;
    
    [Header("조건")]
    public RelicActiveType relicActiveType;
    [DrawIf("relicActiveType", RelicActiveType.AfterGetStatusEffect)]
    public StatusEffectSO statusEffectSO;
    
    [Header("효과")]
    public List<BehaviourInfo> behaviourInfos;
    public List<GameAction> actions;
    
    public void ExecuteActions()
    {
        foreach (var action in actions)
        {
            action.Execute();
        }
    }
}

public enum RelicActiveType
{
    Gain,
    StartGame,
    TurnChange,
    AfterAttack,
    AfterHit,
    AfterGetStatusEffect,
    AfterAction,
    EndGame,
}