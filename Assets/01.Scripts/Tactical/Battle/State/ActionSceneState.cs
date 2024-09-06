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
        // 카메라 설정 및 연출
        PerformCameraProduction();

        // 유닛 정보 설정
        var from = Turn.isPlayer ? owner.player : owner.enemy;
        var to = Turn.isPlayer ? owner.enemy : owner.player;

        // 행동 실행
        ExecuteAction(from, to);
        
        yield return YieldInstructionCache.WaitForSeconds(1.2f);
        
        // 카메라 리셋 및 UI 업데이트
        ResetCameraProduction();
        UpdateUI();

        yield return null;
        
        owner.ChangeState<TurnChangeState>();
    }

    // 카메라 연출 실행
    private void PerformCameraProduction()
    {
        var orderMultiplier = owner.player ? 1 : -1;
        owner.mainCameraMovement.ProductionAtTime(new Vector3(0, 0.35f, -10), 3 * orderMultiplier, 4.6f);
        owner.highlightCameraMovement.ProductionAtTime(new Vector3(0, 0.35f, -10), 0, 4f);
        owner.mainCameraVolumeSettings.SetVolume();
    }

    // 카메라와 UI 초기화
    private void ResetCameraProduction()
    {
        owner.mainCameraMovement.ProductionAtTime(new Vector3(0, 0, -10), 0, 5, true);
        owner.highlightCameraMovement.ProductionAtTime(new Vector3(0, 0, -10), 0, 5, true);
        owner.mainCameraVolumeSettings.ResetVolume();
    }

    // UI 업데이트
    private void UpdateUI()
    {
        owner.diceResultPanelController.Hide();
        owner.interactionPanelController.Show();
        owner.topPanelController.Show();
    }

    // 행동 실행
    private void ExecuteAction(Unit from, Unit to)
    {
        var fromBehaviours = CreateBehaviours(from);
        var toBehaviours = CreateBehaviours(to);

        // 피해 계산
        var fromTotalDamage = CalculateDamage(fromBehaviours, toBehaviours, from, to);
        var toTotalDamage = CalculateDamage(toBehaviours, fromBehaviours, to, from);
        
        // 카메라 진동 및 유닛 액션 실행
        ExecuteUnitActions(from, to, fromTotalDamage, toTotalDamage, fromBehaviours, toBehaviours);
    }

    // 행동 리스트 생성
    private List<Behaviour> CreateBehaviours(Unit unit)
    {
        var behaviours = new List<Behaviour>();
        foreach (var (behaviourInfo, value) in unit.behaviourValues)
        {
            var behaviour = (Behaviour)Activator.CreateInstance(behaviourInfo.behaviourType.GetBehaviourClass());
            behaviour.compareInfo = behaviourInfo.compareInfo;
            behaviour.onSelf = behaviourInfo.onSelf;
            behaviour.value = value;
            if (behaviour is StatusEffectBehaviour statusEffectBehaviour)
            {
                statusEffectBehaviour.statusEffectSO = behaviourInfo.statusEffectSO;
            }
            behaviours.Add(behaviour);
        }
        return behaviours;
    }

    // 피해 계산
    private int CalculateDamage(List<Behaviour> attackerBehaviours, List<Behaviour> defenderBehaviours, Unit attacker, Unit defender)
    {
        return attackerBehaviours
                .Where(b => !b.onSelf && b.IsSatisfied(attacker.behaviourValues, defender.behaviourValues))
                .Aggregate(0, (current, behaviour) => behaviour.GetValue(current)) +
               defenderBehaviours
                .Where(b => b.onSelf && b.IsSatisfied(defender.behaviourValues, attacker.behaviourValues))
                .Aggregate(0, (current, behaviour) => behaviour.GetValue(current));
    }

    // 유닛 행동 실행
    private void ExecuteUnitActions(Unit from, Unit to, int fromTotalDamage, int toTotalDamage, List<Behaviour> fromBehaviours, List<Behaviour> toBehaviours)
    {
        var isAvoid = IsSatisfiedBehaviours(to, from, toBehaviours, BehaviourType.Avoid);
        
        if (fromTotalDamage > 0 && !isAvoid)
        {
            if (to.TryGetComponent<Health>(out var toHealth))
            {
                toHealth.OnDamage(fromTotalDamage);
                owner.hudController.PopDamage(to.transform.position, fromTotalDamage);
            }
            if (to.TryGetComponent<StatusEffect>(out var toStatusEffect))
            {
                //toStatusEffect.AddEffect();
                owner.hudController.PopDamage(to.transform.position, fromTotalDamage);
            }
        }

        if (toTotalDamage > 0)
        {
            if (from.TryGetComponent<Health>(out var fromHealth))
            {
                fromHealth.OnDamage(toTotalDamage);
                owner.hudController.PopDamage(from.transform.position, toTotalDamage);
            }
        }
        
        owner.mainCameraMovement.VibrationForTime(0.65f);
        owner.highlightCameraMovement.VibrationForTime(0.5f);

        if (!from.TryGetComponent<Act>(out var fromAct) || !to.TryGetComponent<Act>(out var toAct)) return;
        
        fromAct.PerformAction(from.unitSO.attacks.Random());
        toAct.PerformAction(isAvoid ? to.unitSO.avoids.Random() : to.unitSO.hits.Random());
    }
    
    private bool IsSatisfiedBehaviours(Unit from, Unit to, List<Behaviour> behaviours, BehaviourType behaviourType)
    {
        return behaviours.Where(b => b.GetType() == behaviourType.GetBehaviourClass())
            .Any(b => b.IsSatisfied(from.behaviourValues, to.behaviourValues));
    }
}