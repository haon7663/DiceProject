using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnChangeState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(ChangeTurn());
    }

    private IEnumerator ChangeTurn()
    {
        if (Turn.actor)
            Turn.ChangeTurn(Turn.isPlayer ? owner.enemy : owner.player);
        else
            Turn.ChangeTurn(owner.player);
        
        yield return StartCoroutine(owner.turnOrderController.Show(Turn.isPlayer));
        
        owner.ChangeState<UnitChangeState>();
    }
}
