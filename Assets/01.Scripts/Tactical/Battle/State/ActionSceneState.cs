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
            
        owner.mainCameraMovement.VibrationForTime(0.65f);
        owner.mainCameraMovement.ProductionAtTime(new Vector3(0, 0.35f, -10), 6 * orderMultiplier, 4.6f);
        owner.highlightCameraMovement.VibrationForTime(0.5f);
        owner.highlightCameraMovement.ProductionAtTime(new Vector3(0, 0.35f, -10), 2 * orderMultiplier, 4f);
        owner.mainCameraVolumeSettings.SetVolume();
        
        var from = Turn.isPlayer ? owner.player : owner.enemy;
        var to = Turn.isPlayer ? owner.enemy : owner.player;
        
        if (from.TryGetComponent<Act>(out var fromAct) && to.TryGetComponent<Act>(out var toAct))
        {
            fromAct.PerformAction(from.unitSO.attacks.Random());
            toAct.PerformAction(to.unitSO.hits.Random());
            //임시로 랜덤 처리해둠, 나중에는 카드에 맞게 수정
            
            var behaviours = new List<Behaviour>();
            foreach (var (cardEffect, value) in from.values)
            {
                switch (cardEffect.behaviourType)
                {
                    case BehaviourType.Attack:
                        behaviours.Add(new AttackBehaviour(value, cardEffect.compareType));
                        break;
                    case BehaviourType.Defence:
                        behaviours.Add(new DefenceBehaviour(value, cardEffect.compareType));
                        break;
                    case BehaviourType.Avoid:
                        behaviours.Add(new AvoidBehaviour(value, cardEffect.compareType));
                        break;
                    case BehaviourType.Counter:
                        behaviours.Add(new CounterBehaviour(value, cardEffect.compareType));
                        break;
                    case BehaviourType.StatusEffect:
                        behaviours.Add(new StatusEffectBehaviour(value, cardEffect.compareType));
                        break;
                }
            }
            
            var finalValue = behaviours.Sum(behaviour => behaviour.CalculateValue());
            
            /*var behaviours = new List<Behaviour>();
            behaviours.AddRange(from.cardSO.cardEffects.Select(effect => effect.behaviour));
            behaviours.AddRange(to.cardSO.cardEffects.Select(effect => effect.behaviour));*/
            //var (success, value) = BattleSystem.CompareBehaviours(behaviours);
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
}
