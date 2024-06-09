using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayStatusEffectBundle : MonoBehaviour
{
    [SerializeField] private Transform[] layoutGroup;
    [SerializeField] private DisplayStatusEffect displayStatusEffectPrefab;
    [SerializeField] private List<DisplayStatusEffect> displayEffects = new List<DisplayStatusEffect>();

    public void AddEffect(StatusEffectSO statusEffectSO)
    {
        var displayEffect = Instantiate(displayStatusEffectPrefab);
        displayEffect.Setup(statusEffectSO);
        displayEffects.Add(displayEffect);
        SetOrder();
    }

    public void UpdateEffects()
    {
        foreach (var displayEffect in displayEffects)
        {
            displayEffect.UpdateSetup();
        }
        SetOrder();
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
        SetOrder();
    }

    private void SetOrder()
    {
        for(var i = 0; i < displayEffects.Count; i++)
        {
            displayEffects[i].transform.SetParent(layoutGroup[i / 4]);
            displayEffects[i].transform.localScale = Vector3.one;
        }
    }
}
