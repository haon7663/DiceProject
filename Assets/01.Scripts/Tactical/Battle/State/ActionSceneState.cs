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

        ExecuteAction(from, to);
        
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

    private void ExecuteAction(Unit from, Unit to)
    {
        if (!from.TryGetComponent<Act>(out var fromAct) || !to.TryGetComponent<Act>(out var toAct)) return;
        
        var fromBehaviours = new List<Behaviour>();
        foreach (var (behaviourInfo, value) in from.behaviourValues)
        {
            var behaviour = new AttackBehaviour(behaviourInfo.compareInfo, value, behaviourInfo.onSelf);
            fromBehaviours.Add(behaviour);
        }

        var toBehaviours = new List<Behaviour>();
        foreach (var (behaviourInfo, value) in to.behaviourValues)
        {
            var behaviour = new AttackBehaviour(behaviourInfo.compareInfo, value, behaviourInfo.onSelf);
            toBehaviours.Add(behaviour);
        }

        var toTotalValue = 0;
        var fromTotalValue = 0;
        var isAvoid = false;
        var isCounter = false;

        foreach (var behaviour in fromBehaviours)
        {
            var targetValue = from.behaviourValues.Values.Sum();
            var opponentValue = to.behaviourValues.Values.Sum();

            int totalValue = 0;

            switch (behaviour.BehaviourType)
            {
                case BehaviourType.Attack:
                    totalValue = GetTotalValue(new List<Behaviour> { behaviour }, BehaviourType.Attack, targetValue, opponentValue);
                    break;
                case BehaviourType.Defence:
                    totalValue = -GetTotalValue(new List<Behaviour> { behaviour }, BehaviourType.Defence, targetValue, opponentValue);
                    break;
                case BehaviourType.Avoid:
                    if (HasBehaviour(new List<Behaviour> { behaviour }, BehaviourType.Avoid, targetValue, opponentValue))
                        isAvoid = true;
                    break;
                case BehaviourType.Counter:
                    if (HasBehaviour(new List<Behaviour> { behaviour }, BehaviourType.Counter, targetValue, opponentValue))
                        isCounter = true;
                    break;
            }

            if (behaviour.OnSelf)
                toTotalValue += totalValue;
            else
                fromTotalValue += totalValue;
        }
        
        foreach (var behaviour in toBehaviours)
        {
            var targetValue = to.behaviourValues.Values.Sum();
            var opponentValue = from.behaviourValues.Values.Sum();

            int totalValue = 0;

            switch (behaviour.BehaviourType)
            {
                case BehaviourType.Attack:
                    totalValue = GetTotalValue(new List<Behaviour> { behaviour }, BehaviourType.Attack, targetValue, opponentValue);
                    break;
                case BehaviourType.Defence:
                    totalValue = -GetTotalValue(new List<Behaviour> { behaviour }, BehaviourType.Defence, targetValue, opponentValue);
                    break;
                case BehaviourType.Avoid:
                    if (HasBehaviour(new List<Behaviour> { behaviour }, BehaviourType.Avoid, targetValue, opponentValue))
                        isAvoid = true;
                    break;
                case BehaviourType.Counter:
                    if (HasBehaviour(new List<Behaviour> { behaviour }, BehaviourType.Counter, targetValue, opponentValue))
                        isCounter = true;
                    break;
            }

            if (behaviour.OnSelf)
                fromTotalValue += totalValue;
            else
                toTotalValue += totalValue;
        }

        if (toTotalValue > 0)
        {
            if (from.TryGetComponent<Health>(out var health))
                health.OnDamage(toTotalValue);
            owner.hudController.PopDamage(from.transform.position, toTotalValue);
        }

        if (fromTotalValue > 0 && !isAvoid)
        {
            if (to.TryGetComponent<Health>(out var health))
                health.OnDamage(fromTotalValue);
            owner.hudController.PopDamage(to.transform.position, fromTotalValue);
        }

        if (HasBehaviour(fromBehaviours, BehaviourType.Attack, from.behaviourValues.Values.Sum(),
                to.behaviourValues.Values.Sum()))
        {
            owner.mainCameraMovement.VibrationForTime(0.65f);
            owner.highlightCameraMovement.VibrationForTime(0.5f);

            fromAct.PerformAction(from.unitSO.attacks.Random());
            toAct.PerformAction(
                HasBehaviour(toBehaviours, BehaviourType.Avoid, to.behaviourValues.Values.Sum(),
                    from.behaviourValues.Values.Sum())
                    ? to.unitSO.avoids.Random()
                    : to.unitSO.hits.Random());
        }
    }

    private int GetTotalValue(List<Behaviour> behaviours, BehaviourType behaviourType, int fromValue, int toValue)
    {
        return behaviours
            .Where(b => b.BehaviourType == behaviourType && b.IsSatisfied(fromValue, toValue))
            .Sum(b => b.Value);
    }

    private bool HasBehaviour(List<Behaviour> behaviours, BehaviourType behaviourType, int fromValue, int toValue)
    {
        return behaviours.Any(b => b.BehaviourType == behaviourType && b.IsSatisfied(fromValue, toValue));
    }
}
