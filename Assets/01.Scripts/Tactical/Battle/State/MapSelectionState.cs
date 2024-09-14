public class MapSelectionState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.interactionPanelController.Enable();
        owner.mapController.Show();
    }
}