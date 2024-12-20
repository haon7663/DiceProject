using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleState : State
{
    protected BattleController owner;
    
    protected Turn Turn => owner.Turn;
    protected Unit Unit
    {
        get => owner.CurrentUnit;
        set => owner.CurrentUnit = value;
    }
    
    protected Driver driver;
    
    protected virtual void Awake()
    {
        owner = GetComponent<BattleController>();
    }

    public override void Enter()
    {
        driver = Unit ? Unit.GetComponent<Driver>() : null;
        base.Enter();
    }

    protected IEnumerator CheckVictor()
    {
        yield return null;
        switch (owner.Victor)
        {
            case VictorType.Player:
                owner.ChangeState<VictoryState>();
                break;
            case VictorType.Enemy:
                owner.ChangeState<DefeatState>();
                break;
        }
    }
}
