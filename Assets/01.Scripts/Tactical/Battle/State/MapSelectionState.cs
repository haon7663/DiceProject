public class MapSelectionState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.mapController.Show();
    }
}