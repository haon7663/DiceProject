using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleState : State
{
    protected BattleController owner;
    
    protected Turn Turn => owner.Turn;
    protected Unit Unit
    {
        get => owner.Unit;
        set => owner.Unit = value;
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
}
