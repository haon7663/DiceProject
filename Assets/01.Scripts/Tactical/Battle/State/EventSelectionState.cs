using UnityEngine;

public class EventSelectionState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.eventChoicesController.ShowEventChoices(owner.eventData);
        owner.eventChoicesController.OnExecute += EndEvent;
    }

    private void EndEvent()
    {
        owner.ChangeState<MapSelectionState>();
        print("EndEvent");
    }
}