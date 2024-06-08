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
        UIManager.inst.PopStatusEffectText(transform.position, effectSO, stack);
        if (!enabledEffects.Exists(effect => effect.name == effectSO.name))
        {
            var effect = CreateEffectObject(effectSO);
            effect.ApplyEffect(_creature, stack);
            //상태이상 UI 표기
            displayStatusEffectBundle.AddEffect(effect);
        }
        else
        {
            var effect = enabledEffects.Find(effect => effect.name == effectSO.name);
            if (effect.DuplicateEffect(stack))
            {
                displayStatusEffectBundle.UpdateEffects();
                return;
            }
            displayStatusEffectBundle.UpdateEffects();
            CreateEffectObject(effect).ApplyEffect(_creature, stack);
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
        enabledEffects.Remove(effectSO);
    }
}
