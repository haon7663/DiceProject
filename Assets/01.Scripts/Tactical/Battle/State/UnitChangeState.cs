using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitChangeState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(ChangeUnit());
    }

    private IEnumerator ChangeUnit()
    {
        if (!Unit)
            Unit = owner.enemy;
        else
            Unit = Unit.type == UnitType.Player ? owner.enemy : owner.player;
        
        yield return null;
        
        owner.ChangeState<CommandSelectionState>();
    }
}
