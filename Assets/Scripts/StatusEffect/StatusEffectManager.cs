using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    private Creature _creature;
    public List<StatusEffectSO> enabledEffects = new();

    [SerializeField] private DisplayStatusEffectBundle displayStatusEffectBundle;

    private void Start()
    {
        _creature = GetComponent<Creature>();
    }

    public void AddEffect(StatusEffectSO effectSO, int stack)
    {
        UIManager.Inst.PopStatusEffectText(transform.position, effectSO, stack);
        if (!enabledEffects.Exists(effect => effect.name == effectSO.name))
        {
            var newEffect = CreateEffectObject(effectSO);
            newEffect.ApplyEffect(_creature, stack);
            //상태이상 UI 표기
            displayStatusEffectBundle.AddEffect(newEffect);
        }
        else
        {
            var effect = enabledEffects.Find(effect => effect.name == effectSO.name);
            if (effect.DuplicateEffect(stack))
            {
                displayStatusEffectBundle.UpdateEffects();
                return;
            }

            var newEffect = CreateEffectObject(effect);
            newEffect.ApplyEffect(_creature, stack);
            displayStatusEffectBundle.AddEffect(newEffect);
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
        for (var i = enabledEffects.Count - 1; i >= 0; i--)
        {
            var effect = enabledEffects[i];
            effect.UpdateEffect(_creature);
            effect.UpdateCall(_creature);
        }
        displayStatusEffectBundle.UpdateEffects();
    }

    public void RemoveEffect(StatusEffectSO effectSO)
    {
        displayStatusEffectBundle.RemoveEffect(effectSO);
        enabledEffects.Remove(effectSO);
    }
}
