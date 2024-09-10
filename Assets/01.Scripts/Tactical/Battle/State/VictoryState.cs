public class VictoryState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.rewardPanelController.Show();
    }
}