using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStatusEffectState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(UpdateEffects());
    }

    private IEnumerator UpdateEffects()
    {
        yield return StartCoroutine(UpdateEffect(owner.player));
        yield return StartCoroutine(UpdateEffect(owner.enemy));
        
        yield return null;
        
        owner.ChangeState<UnitChangeState>();
    }

    private IEnumerator UpdateEffect(Unit unit)
    {
        if (!unit.TryGetComponent<StatusEffect>(out var statusEffect)) yield break;
        
        for (var i = 0; i < statusEffect.enabledEffects.Count; i++)
        {
            if (!unit) continue;
            
            var effect = statusEffect.enabledEffects[i];
            if (effect.GetCurrentValue() == 0) continue;
            owner.hudController.PopStatusEffect(unit.transform.position, effect.GetCurrentValue(), effect.sprite);

            if (effect.effectSprite)
            {
                var vfxPrefab = Resources.Load<StatusEffectVFX>("Effects/StatusEffect VFX");
                var vfxObject = Instantiate(vfxPrefab, unit.transform);
                vfxObject.transform.localPosition = new Vector3(0, 1.2f);
                vfxObject.Initialize(effect.effectSprite);
            }
            
            statusEffect.UpdateEffect(effect);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
