using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusActionState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Focus());
    }

    private IEnumerator Focus()
    {
        owner.interactionPanelController.Hide();
        owner.topPanelController.Hide();
        owner.cardController.UseCards();
        
        yield return YieldInstructionCache.WaitForSeconds(1f);
        
        owner.ChangeState<DiceRollingState>();
    }
}
