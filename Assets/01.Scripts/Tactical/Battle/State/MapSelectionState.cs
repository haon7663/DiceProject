public class MapSelectionState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        
        DataManager.Inst.Save();
        TutorialDataContainer.Inst.Save();
        
        owner.interactionPanelController.Enable();
        owner.mapController.Show();
        owner.tutorialPanelController.TryToShow("맵 이동");
    }
}