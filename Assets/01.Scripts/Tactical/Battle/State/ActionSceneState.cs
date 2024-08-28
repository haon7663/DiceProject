using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSceneState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Action());
    }

    private IEnumerator Action()
    {
        owner.interactionPanelController.Hide();
        owner.topPanelController.Hide();
        yield return YieldInstructionCache.WaitForSeconds(1f);
            
        owner.mainCameraMovement.VibrationForTime(0.65f);
        owner.mainCameraMovement.ProductionAtTime(new Vector3(-0.4f, 0.35f, -10), 3, 4.4f);
        owner.highlightCameraMovement.VibrationForTime(0.5f);
        owner.highlightCameraMovement.ProductionAtTime(new Vector3(-0.4f, 0.35f, -10), 1, 4f);
        owner.mainCameraVolumeSettings.SetVolume();
        
        var from = Turn.isPlayer ? owner.player : owner.enemy;
        var to = Turn.isPlayer ? owner.enemy : owner.player;
        
        if (from.TryGetComponent<Act>(out var fromAct) && to.TryGetComponent<Act>(out var toAct))
        {
            fromAct.AttackAction();
            toAct.HitAction();
        }
        
        yield return YieldInstructionCache.WaitForSeconds(1.2f);
        
        owner.mainCameraMovement.ProductionAtTime(new Vector3(0, 0, -10), 0, 5, true);
        owner.highlightCameraMovement.ProductionAtTime(new Vector3(0, 0, -10), 0, 5, true);
        owner.mainCameraVolumeSettings.ResetVolume();
        
        owner.interactionPanelController.Show();
        owner.topPanelController.Show();

        yield return null;
    }
}
