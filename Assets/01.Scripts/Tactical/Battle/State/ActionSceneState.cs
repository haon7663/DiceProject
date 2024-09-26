using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
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
        // 유닛 정보 설정
        var from = Turn.isPlayer ? owner.player : owner.enemy;
        var to = Turn.isPlayer ? owner.enemy : owner.player;
        
        owner.dialogController.Show();
        owner.diceResultPanelController.Hide();
        
        // 행동 실행
        ExecuteAction(from, to);
        yield return YieldInstructionCache.WaitForSeconds(1.2f);
        
        
        if (BehaviourType.Attack.IsSatisfiedBehaviours(from, to) &&
            BehaviourType.Counter.IsSatisfiedBehaviours(from, to))
        {
            TakeCounter(from, to);
            ExecuteUnitActions(to, from);
            PerformCameraProduction(to, from);
            yield return YieldInstructionCache.WaitForSeconds(1.2f);
        }
        
        // 카메라 리셋 및 UI 업데이트
        ResetUnitActions(from, to);
        ResetCameraProduction();

        yield return null;
        
        owner.ChangeState<DiversionActionState>();
    }

    // 행동 실행
    private void ExecuteAction(Unit from, Unit to)
    {
        TakeDamage(from, to);
        TakeStatusEffect(from, to);
        ExecuteUnitActions(from, to);
        PerformCameraProduction(from, to);
    }
    
    // 피해 계산
    private int CalculateDamage(Unit from, Unit to)
    {
        var fromBehaviours = BehaviourExtension.CreateBehaviours(from, to);
        var toBehaviours = BehaviourExtension.CreateBehaviours(to, from);
        
        return fromBehaviours
                .Where(b => !b.onSelf && b.IsSatisfied(from.behaviourValues, to.behaviourValues))
                .Aggregate(0, (current, behaviour) => behaviour.GetValue(current)) +
               toBehaviours
                .Where(b => b.onSelf && b.IsSatisfied(to.behaviourValues, from.behaviourValues))
                .Aggregate(0, (current, behaviour) => behaviour.GetValue(current));
    }

    private void TakeDamage(Unit from, Unit to)
    {
        var fromDefaultDamage = CalculateDamage(from, to);
        var toDefaultDamage = CalculateDamage(to, from);
 
        var fromStatsDamage = from.Stats[StatType.GetDamage].GetValue(fromDefaultDamage);
        fromStatsDamage = to.Stats[StatType.TakeDamage].GetValue(fromStatsDamage);
        
        var toStatsDamage = to.Stats[StatType.GetDamage].GetValue(toDefaultDamage);
        toStatsDamage = from.Stats[StatType.TakeDamage].GetValue(toStatsDamage);
        
        var fromTotalDamage = fromStatsDamage > 1 ? fromStatsDamage : 1;
        var toTotalDamage = toStatsDamage > 1 ? toStatsDamage : 1;
        
        if (BehaviourType.Attack.IsSatisfiedBehaviours(from, to))
        {
            if (BehaviourType.Avoid.IsSatisfiedBehaviours(from, to))
            {
                owner.hudController.PopAvoid(to.transform.position);
                owner.dialogController.GenerateDialog($"{to.unitSO.name}은(는) {from.cardSO.cardName}을(를) 회피했다.".ConvertKoreaStringJongSung());
            }
            else
            {
                if (to.TryGetComponent<Health>(out var toHealth))
                {
                    var defenceValue = BehaviourType.Defence.GetSatisfiedBehavioursSum(from, to);
                    
                    toHealth.OnDamage(fromTotalDamage);
                    owner.hudController.PopDamage(to.transform.position, fromTotalDamage);

                    var dialog = $"{to.unitSO.name}은(는) {from.cardSO.cardName}으로(로) {fromTotalDamage}의 피해를 입었다. ";
                    
                    var defenceDialog = defenceValue > 0 ? $"({defenceValue} 방어함)" : "";

                    var changedValue = fromStatsDamage - fromDefaultDamage;
                    var changedValueDialog = changedValue > 0 ? $"({changedValue} 증가됨)" : (changedValue < 0 ? $"({-changedValue} 감소됨)" : "");

                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append(dialog);
                    stringBuilder.Append(defenceDialog);
                    stringBuilder.Append(changedValueDialog);

                    var convertedString = stringBuilder.ToString().ConvertKoreaStringJongSung();
                    owner.dialogController.GenerateDialog(convertedString);
                }
            }
        }

        if (BehaviourType.Attack.IsSatisfiedBehaviours(to, from))
        {
            if (BehaviourType.Avoid.IsSatisfiedBehaviours(to, from))
            {
                owner.hudController.PopAvoid(from.transform.position);
                owner.dialogController.GenerateDialog($"{from.unitSO.name}은(는) {to.cardSO.cardName}을(를) 회피했다.".ConvertKoreaStringJongSung());
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
    
    private List<(Behaviour value, bool onSelf)> GetStatusEffectBehaviours(Unit from, Unit to)
    {
        var fromBehaviours = BehaviourExtension.CreateBehaviours(from, to);
        var toBehaviours = BehaviourExtension.CreateBehaviours(to, from);

        var fromStatusEffectBehaviours = fromBehaviours.Where(b => b is StatusEffectBehaviour);
        var toStatusEffectBehaviours = toBehaviours.Where(b => b is StatusEffectBehaviour);

        var behaviours = new List<(Behaviour behaviour, bool isOnSelf)>();
        
        behaviours.AddRange(fromStatusEffectBehaviours
            .Where(b => !b.onSelf && b.IsSatisfied(from.behaviourValues, to.behaviourValues))
            .Select(b => (b, false)));
        
        behaviours.AddRange(toStatusEffectBehaviours
            .Where(b => b.onSelf && b.IsSatisfied(to.behaviourValues, from.behaviourValues))
            .Select(b => (b, true)));

        return behaviours;
    }

    private void TakeStatusEffect(Unit from, Unit to)
    {
        if (to.TryGetComponent<StatusEffect>(out var toStatusEffect))
        {
            for (var i = toStatusEffect.enabledEffects.Count - 1; i >= 0; i--)
            {
                var effect = toStatusEffect.enabledEffects[i];
                switch (effect.statusEffectStackType)
                {
                    case StatusEffectStackType.AfterAttack:
                        if (BehaviourType.Attack.IsSatisfiedBehaviours(to, from))
                            effect.UpdateStack(to);
                        break;
                    case StatusEffectStackType.AfterHit:
                        if (BehaviourType.Attack.IsSatisfiedBehaviours(from, to))
                            effect.UpdateStack(to);
                        break;
                    case StatusEffectStackType.AfterAction:
                        effect.UpdateStack(to);
                        break;
                }
            }

            if (!BehaviourType.Avoid.IsSatisfiedBehaviours(from, to))
            {
                foreach (var behaviour in GetStatusEffectBehaviours(from, to))
                {
                    if (behaviour.value is StatusEffectBehaviour statusEffectBehaviour)
                    {
                        toStatusEffect.AddEffect(statusEffectBehaviour.statusEffectSO, statusEffectBehaviour.value);

                        var takeCardName = behaviour.onSelf ? to.cardSO.cardName : from.cardSO.cardName;
                        var giveOrTake = behaviour.onSelf ? "얻었다" : "입었다";
                        var dialog = $"{to.unitSO.name}은(는) {takeCardName}으로(로) {statusEffectBehaviour.statusEffectSO.label}({statusEffectBehaviour.value}) 상태를 {giveOrTake}.";
                        owner.dialogController.GenerateDialog(dialog.ConvertKoreaStringJongSung());
                    }
                }
            }
            else
            {
                if (!BehaviourType.Attack.IsSatisfiedBehaviours(from, to))
                {
                    owner.hudController.PopAvoid(to.transform.position);
                    owner.dialogController.GenerateDialog($"{to.unitSO.name}은(는) {from.cardSO.cardName}을(를) 회피했다."
                        .ConvertKoreaStringJongSung());
                }
            }
        }
        
        if (from.TryGetComponent<StatusEffect>(out var fromStatusEffect))
        {
            for (var i = fromStatusEffect.enabledEffects.Count - 1; i >= 0; i--)
            {
                var effect = fromStatusEffect.enabledEffects[i];
                switch (effect.statusEffectStackType)
                {
                    case StatusEffectStackType.AfterAttack:
                        if (BehaviourType.Attack.IsSatisfiedBehaviours(from, to))
                            effect.UpdateStack(from);
                        break;
                    case StatusEffectStackType.AfterHit:
                        if (BehaviourType.Attack.IsSatisfiedBehaviours(to, from))
                            effect.UpdateStack(from);
                        break;
                    case StatusEffectStackType.AfterAction:
                        effect.UpdateStack(from);
                        break;
                }
            }

            if (!BehaviourType.Avoid.IsSatisfiedBehaviours(to, from))
            {
                foreach (var behaviour in GetStatusEffectBehaviours(to, from))
                {
                    if (behaviour.value is StatusEffectBehaviour statusEffectBehaviour)
                    {
                        fromStatusEffect.AddEffect(statusEffectBehaviour.statusEffectSO, statusEffectBehaviour.value);
                    
                        var takeCardName = behaviour.onSelf ? from.cardSO.cardName : to.cardSO.cardName;
                        var giveOrTake = behaviour.onSelf ? "얻었다" : "입혔다";
                        var dialog = $"{from.unitSO.name}은(는) {takeCardName}으로(로) {statusEffectBehaviour.statusEffectSO.label}({statusEffectBehaviour.value}) 상태를 {giveOrTake}.";
                        owner.dialogController.GenerateDialog(dialog.ConvertKoreaStringJongSung());
                    }
                }
            }
        }
    }
    
    private List<Behaviour> GetCounterEffectBehaviours(Unit from, Unit to)
    {
        var fromBehaviours = BehaviourExtension.CreateBehaviours(from, to);
        var toBehaviours = BehaviourExtension.CreateBehaviours(to, from);

        var fromCounterBehaviours = fromBehaviours.Where(b => b is CounterBehaviour);
        var toCounterBehaviours = toBehaviours.Where(b => b is CounterBehaviour);

        var behaviours = new List<Behaviour>();
        
        behaviours.AddRange(fromCounterBehaviours
            .Where(b => !b.onSelf && b.IsSatisfied(from.behaviourValues, to.behaviourValues)));
        
        behaviours.AddRange(toCounterBehaviours
            .Where(b => b.onSelf && b.IsSatisfied(to.behaviourValues, from.behaviourValues)));

        return behaviours;
    }


    private void TakeCounter(Unit from, Unit to)
    {
        if (from.TryGetComponent<Health>(out var fromHealth))
        {
            foreach (var behaviour in GetCounterEffectBehaviours(from, to))
            {
                if (behaviour is CounterBehaviour counterBehaviour)
                {
                    var defenceValue = BehaviourType.Defence.GetSatisfiedBehavioursSum(to, from);

                    var defaultDamage = counterBehaviour.value - defenceValue;
        
                    
                    var totalDamage = to.Stats[StatType.GetDamage].GetValue(defaultDamage);
                    totalDamage = from.Stats[StatType.TakeDamage].GetValue(totalDamage);
                    totalDamage = totalDamage > 1 ? totalDamage : 1;
                        
                        
                    fromHealth.OnDamage(totalDamage);
                    owner.hudController.PopDamage(from.transform.position, totalDamage);

                    var dialog = $"{to.unitSO.name}은(는) {totalDamage}의 피해로 반격했다. ";
                    
                    var defenceDialog = defenceValue > 0 ? $"({defenceValue} 방어함)" : "";

                    var changedValue = totalDamage - defaultDamage;
                    var changedValueDialog = changedValue > 0 ? $"({changedValue} 증가됨)" : (changedValue < 0 ? $"({-changedValue} 감소됨)" : "");

                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append(dialog);
                    stringBuilder.Append(defenceDialog);
                    stringBuilder.Append(changedValueDialog);

                    var convertedString = stringBuilder.ToString().ConvertKoreaStringJongSung();
                    owner.dialogController.GenerateDialog(convertedString);
                }
            }
        }
    }

    // 유닛 행동 실행
    private void ExecuteUnitActions(Unit from, Unit to)
    {
        var isAvoid = BehaviourType.Avoid.IsSatisfiedBehaviours(from, to);

        if (!from.TryGetComponent<Act>(out var fromAct) || !to.TryGetComponent<Act>(out var toAct)) return;

        var fromActAnimation = from.unitSO.attacks;
        var toActAnimation = isAvoid ? to.unitSO.avoids : to.unitSO.hits;
        var fromIndex = Mathf.Clamp(from.cardSO.animationCount, 0, fromActAnimation.Count - 1);
        var toIndex = Mathf.Clamp(from.cardSO.animationCount, 0, toActAnimation.Count - 1);
            
        fromAct.PerformAction(fromActAnimation[fromIndex]);
        toAct.PerformAction(toActAnimation[toIndex]);
    }
    
    // 카메라 연출 실행
    private void PerformCameraProduction(Unit from, Unit to)
    {
        var intensity = 5f;
        if (BehaviourType.Attack.IsSatisfiedBehaviours(from, to))
        {
            if (CalculateDamage(from, to) > 7)
            {
                owner.mainCameraVolumeSettings.SetGrayVolume();
                owner.mainCameraMovement.VibrationForTime(0.85f);
                owner.highlightCameraMovement.VibrationForTime(0.7f);
            }
            else
            {
                intensity = 2.5f;
                owner.mainCameraVolumeSettings.SetNormalVolume();
                owner.mainCameraMovement.VibrationForTime(0.65f);
                owner.highlightCameraMovement.VibrationForTime(0.5f);
            }
        }
        else
        {
            intensity = 0f;
            owner.mainCameraVolumeSettings.SetNormalVolume();
            owner.mainCameraMovement.VibrationForTime(0.45f);
            owner.highlightCameraMovement.VibrationForTime(0.35f);
        }
        
        var orderMultiplier = Turn.isPlayer ? 1 : -1;
        owner.mainCameraMovement.ProductionAtTime(new Vector3(0, 0.35f, -10), -intensity * 0.5f * orderMultiplier, 4.6f);
        owner.highlightCameraMovement.ProductionAtTime(new Vector3(0.2f * orderMultiplier, 0.35f, -10), -intensity * orderMultiplier, 4f);
    }

    private void ResetUnitActions(Unit from, Unit to)
    {
        if (!from.TryGetComponent<Act>(out var fromAct) || !to.TryGetComponent<Act>(out var toAct)) return;
            
        fromAct.ResetAction();
        toAct.ResetAction();
    }
    
    // 카메라와 UI 초기화
    private void ResetCameraProduction()
    {
        owner.mainCameraMovement.ProductionAtTime(new Vector3(0, 0, -10), 0, 5, true);
        owner.highlightCameraMovement.ProductionAtTime(new Vector3(0, 0, -10), 0, 5, true);
        owner.mainCameraVolumeSettings.ResetVolume();
    }
}