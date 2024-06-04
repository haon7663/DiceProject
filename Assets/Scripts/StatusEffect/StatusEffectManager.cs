using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    public List<StatusEffectSO> enabledEffects = new();
    
    public void AddEffect(StatusEffectSO effectSO, int duration)
    {
        if (!enabledEffects.Exists(effect => effect.name == effectSO.name))
        {
            var effect = CreateEffectObject(effectSO);
            effect.ApplyEffect(gameObject, duration);
            //상태이상 UI 표기
        }
        else
        {
            var effect = enabledEffects.Find(effect => effect.name == effectSO.name);
            if (effect.DuplicateEffect(duration))
                return;
            
            CreateEffectObject(effectSO).ApplyEffect(gameObject, duration);
        }
    }

    private StatusEffectSO CreateEffectObject(StatusEffectSO effectSO)
    {
        var effect = Instantiate(effectSO);
        enabledEffects.Add(effect);
        return effect;
    }
    
    public void UpdateEffects()
    {
        foreach (var effect in enabledEffects)
        {
            effect.UpdateEffect(gameObject);
        }
    }

    public void RemoveEffect(StatusEffectSO effectSO)
    {
        enabledEffects.Remove(effectSO);
    }
}
