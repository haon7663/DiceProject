using System.Collections;

public class PrepareBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(PrepareBattle());
    }

    private IEnumerator PrepareBattle()
    {
        if (owner.player.TryGetComponent<Act>(out var playerAct))
            playerAct.Init();
        if (owner.enemy.TryGetComponent<Act>(out var enemyAct))
            enemyAct.Init();

        yield return YieldInstructionCache.WaitForSeconds(1f);
        
        owner.ChangeState<TurnChangeState>();
    }
}