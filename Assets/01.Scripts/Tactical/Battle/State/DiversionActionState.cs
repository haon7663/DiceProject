using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiversionActionState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Diversion());
    }

    private IEnumerator Diversion()
    {
        owner.interactionPanelController.Show();
        owner.topPanelController.Show();
        owner.diceResultPanelController.Hide();
        
        yield return YieldInstructionCache.WaitForSeconds(1f);
        
        owner.ChangeState<TurnChangeState>();
    }
}
