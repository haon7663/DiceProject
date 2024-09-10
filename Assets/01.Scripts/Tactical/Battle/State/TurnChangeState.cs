using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnChangeState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        switch (owner.Victor)
        {
            case VictorType.None:
                StartCoroutine(ChangeTurn());
                break;
            default:
                StartCoroutine(CheckVictor());
                break;
        }
    }

    private IEnumerator ChangeTurn()
    {
        if (Turn.actor)
            Turn.ChangeTurn(Turn.isPlayer ? owner.enemy : owner.player);
        else
            Turn.ChangeTurn(owner.player);
        
        owner.cardController.ChangeCardsActive(Turn.isPlayer);
        yield return StartCoroutine(owner.turnOrderController.Show(Turn.isPlayer));
        
        owner.ChangeState<StatusEffectUpdateState>();
    }
}
