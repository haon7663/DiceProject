using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        var orderMultiplier = owner.player ? 1 : -1;
        owner.mainCameraMovement.ProductionAtTime(new Vector3(0, 0.35f, -10), 3 * orderMultiplier, 4.6f);
        owner.highlightCameraMovement.ProductionAtTime(new Vector3(0, 0.35f, -10), 0, 4f);
        owner.mainCameraVolumeSettings.SetVolume();
        
        var from = Turn.isPlayer ? owner.player : owner.enemy;
        var to = Turn.isPlayer ? owner.enemy : owner.player;
        
        if (from.TryGetComponent<Act>(out var fromAct) && to.TryGetComponent<Act>(out var toAct))
        {
            var fromBehaviours = new List<Behaviour>();
            foreach (var (behaviourInfo, value) in from.behaviourValues)
                fromBehaviours.Add(new Behaviour(behaviourInfo.compareInfo, behaviourInfo.behaviourType, value));
            
            var toBehaviours = new List<Behaviour>();
            foreach (var (behaviourInfo, value) in to.behaviourValues)
                toBehaviours.Add(new Behaviour(behaviourInfo.compareInfo, behaviourInfo.behaviourType, value));

            var fromValue = from.behaviourValues.Values.Sum();
            var toValue = to.behaviourValues.Values.Sum();

            var totalAttackValue = GetTotalValue(fromBehaviours, BehaviourType.Attack, fromValue, toValue);
            var totalDefenceValue = GetTotalValue(toBehaviours, BehaviourType.Defence, toValue, fromValue);
            var isAttack = HasBehaviour(fromBehaviours, BehaviourType.Attack, fromValue, toValue);
            var isAvoid = HasBehaviour(toBehaviours, BehaviourType.Avoid, toValue, fromValue);
            var isCounter = HasBehaviour(toBehaviours, BehaviourType.Counter, toValue, fromValue);
            
            var totalValue = totalAttackValue - totalDefenceValue > 1 ? totalAttackValue - totalDefenceValue : 1;
            if (!isAvoid)
            {
                if (to.TryGetComponent<Health>(out var health))
                    health.OnDamage(totalValue);
                owner.hudController.PopDamage(to.transform.position, totalValue);
            }

            if (isAttack)
            {
                owner.mainCameraMovement.VibrationForTime(0.65f);
                owner.highlightCameraMovement.VibrationForTime(0.5f);
                
                fromAct.PerformAction(from.unitSO.attacks.Random());
                toAct.PerformAction(isAvoid ? to.unitSO.avoids.Random() : to.unitSO.hits.Random());
            }
            //임시로 랜덤 처리해둠, 나중에는 카드에 맞게 수정
        }
        
        yield return YieldInstructionCache.WaitForSeconds(1.2f);
        
        owner.mainCameraMovement.ProductionAtTime(new Vector3(0, 0, -10), 0, 5, true);
        owner.highlightCameraMovement.ProductionAtTime(new Vector3(0, 0, -10), 0, 5, true);
        owner.mainCameraVolumeSettings.ResetVolume();
        
        owner.diceResultPanelController.Hide();
        owner.interactionPanelController.Show();
        owner.topPanelController.Show();

        yield return null;
        
        owner.ChangeState<TurnChangeState>();
    }

    private int GetTotalValue(List<Behaviour> behaviours, BehaviourType behaviourType, int fromValue, int toValue)
    {
        return behaviours
            .Where(b => b.BehaviourType == behaviourType && b.IsSatisfied(fromValue, toValue))
            .Select(b => b.Value).Sum();
    }

    private bool HasBehaviour(List<Behaviour> behaviours, BehaviourType behaviourType, int fromValue, int toValue)
    {
        return behaviours.Any(b => b.BehaviourType == behaviourType && b.IsSatisfied(fromValue, toValue));
    }
}
