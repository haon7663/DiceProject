public class DefeatState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.deathPanelController.Show();
    }
}