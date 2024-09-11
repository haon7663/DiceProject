using System.Collections;

public class VictoryState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Victory());
    }

    private IEnumerator Victory()
    {
        yield return null;
        
        owner.ChangeState<RewardState>();
    }
}