using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    public List<StatusEffectSO> enabledEffects = new();
    
    private void AddEffect(StatusEffectSO effectSO, int duration)
    {
        if (!enabledEffects.Exists(effect => effect.effectName == effectSO.effectName))
        {
            var effect = CreateEffectObject(effectSO);
            effect.ApplyEffect(gameObject, duration);
            //상태이상 UI 표기
        }
        else
        {
            var effect = enabledEffects.Find(effect => effect.effectName == effectSO.effectName);
            if (effect.DuplicateEffect(duration))
                return;
            CreateEffectObject(effectSO);
            effect.ApplyEffect(gameObject, duration);
        }
    }

    private StatusEffectSO CreateEffectObject(StatusEffectSO effectSO)
    {
        var effect = Instantiate(effectSO);
        enabledEffects.Add(effect);
        return effect;
    }
    
    public void UpdateEffects(GameObject target)
    {
        foreach (var effect in enabledEffects)
        {
            
        }
    }

    public void RemoveEffect()
    {
        
    }
}
