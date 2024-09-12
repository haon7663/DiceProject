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

        yield return null;
        
        owner.ChangeState<DiversionActionState>();
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

    // 행동 실행
    private void ExecuteAction(Unit from, Unit to)
    {
        TakeDamage(from, to);
        TakeStatusEffect(from, to);
        ExecuteUnitActions(from, to);
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
            behaviour.value = value + behaviourInfo.basicValue;
            if (behaviour is StatusEffectBehaviour statusEffectBehaviour)
            {
                statusEffectBehaviour.statusEffectSO = behaviourInfo.statusEffectSO;
            }
            behaviours.Add(behaviour);
        }
        return behaviours;
    }

    // 피해 계산
    private int CalculateDamage(Unit from, Unit to)
    {
        var fromBehaviours = CreateBehaviours(from);
        var toBehaviours = CreateBehaviours(to);
        
        return fromBehaviours
                .Where(b => !b.onSelf && b.IsSatisfied(from.behaviourValues, to.behaviourValues))
                .Aggregate(0, (current, behaviour) => behaviour.GetValue(current)) +
               toBehaviours
                .Where(b => b.onSelf && b.IsSatisfied(to.behaviourValues, from.behaviourValues))
                .Aggregate(0, (current, behaviour) => behaviour.GetValue(current));
    }

    private void TakeDamage(Unit from, Unit to)
    {
        var fromTotalDamage = CalculateDamage(from, to);
        var toTotalDamage = CalculateDamage(to, from);
        
        fromTotalDamage = fromTotalDamage > 1 ? fromTotalDamage : 1;
        toTotalDamage = toTotalDamage > 1 ? toTotalDamage : 1;
        
        if (IsSatisfiedBehaviours(from, to, BehaviourType.Attack))
        {
            if (IsSatisfiedBehaviours(from, to, BehaviourType.Avoid))
            {
                owner.hudController.PopAvoid(to.transform.position);
            }
            else
            {
                if (to.TryGetComponent<Health>(out var toHealth))
                {
                    toHealth.OnDamage(fromTotalDamage);
                    owner.hudController.PopDamage(to.transform.position, fromTotalDamage);
                }
            }
        }

        if (IsSatisfiedBehaviours(to, from, BehaviourType.Attack))
        {
            if (IsSatisfiedBehaviours(to, from, BehaviourType.Avoid))
            {
                owner.hudController.PopAvoid(from.transform.position);
            }
            else
            {
                if (from.TryGetComponent<Health>(out var fromHealth))
                {
                    fromHealth.OnDamage(toTotalDamage);
                    owner.hudController.PopDamage(from.transform.position, toTotalDamage);
                }
            }
        }
    }
    
    private List<Behaviour> GetStatusEffectBehaviours(Unit from, Unit to)
    {
        var fromBehaviours = CreateBehaviours(from);
        var toBehaviours = CreateBehaviours(to);
        
        var fromStatusEffectBehaviours = fromBehaviours.Where(b => b is StatusEffectBehaviour);
        var toStatusEffectBehaviours = toBehaviours.Where(b => b is StatusEffectBehaviour);
        
        var behaviours = new List<Behaviour>();
        behaviours.AddRange(fromStatusEffectBehaviours.Where(b => !b.onSelf && b.IsSatisfied(from.behaviourValues, to.behaviourValues)));
        behaviours.AddRange(toStatusEffectBehaviours.Where(b => b.onSelf && b.IsSatisfied(to.behaviourValues, from.behaviourValues)));

        return behaviours;
    }

    private void TakeStatusEffect(Unit from, Unit to)
    {
        if (to.TryGetComponent<StatusEffect>(out var toStatusEffect) && !IsSatisfiedBehaviours(from, to, BehaviourType.Avoid))
        {
            foreach (var behaviour in GetStatusEffectBehaviours(from, to))
            {
                if (behaviour is StatusEffectBehaviour statusEffectBehaviour)
                {
                    toStatusEffect.AddEffect(statusEffectBehaviour.statusEffectSO, statusEffectBehaviour.value);
                }
            }
        }
        
        if (from.TryGetComponent<StatusEffect>(out var fromStatusEffect) && !IsSatisfiedBehaviours(to, from, BehaviourType.Avoid))
        {
            foreach (var behaviour in GetStatusEffectBehaviours(to, from))
            {
                if (behaviour is StatusEffectBehaviour statusEffectBehaviour)
                {
                    fromStatusEffect.AddEffect(statusEffectBehaviour.statusEffectSO, statusEffectBehaviour.value);
                }
            }
        }
    }

    // 유닛 행동 실행
    private void ExecuteUnitActions(Unit from, Unit to)
    {
        var isAvoid = IsSatisfiedBehaviours(from, to, BehaviourType.Avoid);
        
        owner.mainCameraMovement.VibrationForTime(0.65f);
        owner.highlightCameraMovement.VibrationForTime(0.5f);

        if (!from.TryGetComponent<Act>(out var fromAct) || !to.TryGetComponent<Act>(out var toAct)) return;
        
        fromAct.PerformAction(from.unitSO.attacks.Random());
        toAct.PerformAction(isAvoid ? to.unitSO.avoids.Random() : to.unitSO.hits.Random());
    }

    private bool IsSatisfiedBehaviours(Unit from, Unit to, BehaviourType behaviourType)
    {
        var fromBehaviours = CreateBehaviours(from);
        var toBehaviours = CreateBehaviours(to);
        
        return fromBehaviours.Where(b => b.GetType() == behaviourType.GetBehaviourClass())
                   .Any(b => !b.onSelf && b.IsSatisfied(from.behaviourValues, to.behaviourValues)) ||
               toBehaviours.Where(b => b.GetType() == behaviourType.GetBehaviourClass())
                   .Any(b => b.onSelf && b.IsSatisfied(to.behaviourValues, from.behaviourValues));
    }
}