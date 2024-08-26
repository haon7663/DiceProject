using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    public event Action OnStatusChanged;
    
    private Unit _unit;
    public List<StatusEffectSO> enabledEffects = new();

    private void Start()
    {
        _unit = GetComponent<Unit>();
    }

    public void AddEffect(StatusEffectSO effectSO, int stack)
    {
        UIManager.Inst.PopStatusEffectText(transform.position, effectSO, stack);
        if (!enabledEffects.Exists(effect => effect.name == effectSO.name))
        {
            var newEffect = CreateEffectObject(effectSO);
            newEffect.ApplyEffect(_unit, stack);
        }
        else
        {
            var effect = enabledEffects.Find(effect => effect.name == effectSO.name);
            if (!effect.DuplicateEffect(stack))
            {
                var newEffect = CreateEffectObject(effect);
                newEffect.ApplyEffect(_unit, stack);
            }
        }
        OnStatusChanged?.Invoke();
    }

    private StatusEffectSO CreateEffectObject(StatusEffectSO effectSO)
    {
        var effect = Instantiate(effectSO);
        enabledEffects.Add(effect);
        return effect;
    }
    
    public void UpdateEffects()
    {
        for (var i = enabledEffects.Count - 1; i >= 0; i--)
        {
            var effect = enabledEffects[i];
            effect.UpdateEffect(_unit);
            effect.UpdateStack(_unit);
        }
        OnStatusChanged?.Invoke();
    }

    public void RemoveEffect(StatusEffectSO effectSO)
    {
        enabledEffects.Remove(effectSO);
        OnStatusChanged?.Invoke();
    }
}
