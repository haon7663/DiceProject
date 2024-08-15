using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DisplayStatusEffectBundle : MonoBehaviour
{
    [SerializeField] private Transform effectParent;
    [SerializeField] private StatusIcon effectPrefab;
    [SerializeField] private List<StatusIcon> displayEffects = new List<StatusIcon>();
    
    public void AddEffect(StatusEffectSO statusEffectSO)
    {
        var displayEffect = Instantiate(effectPrefab, effectParent);
        displayEffect.Init(statusEffectSO);
        displayEffects.Add(displayEffect);
    }

    public void UpdateEffects()
    {
        foreach (var displayEffect in displayEffects)
        {
            displayEffect.UpdateSetup();
        }
    }

    public void RemoveEffect(StatusEffectSO statusEffectSO)
    {
        var displayEffect = displayEffects.Find(displayEffect => displayEffect.StatusEffectSO == statusEffectSO);
        if (displayEffect)
        {
            displayEffects.Remove(displayEffect);
            Destroy(displayEffect.gameObject);
        }
        else
        {
            print("이펙트없어..");
        }
    }
}
