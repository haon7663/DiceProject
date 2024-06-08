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
        for (var i = displayEffects.Count - 1; i >= 0; i--)
        {
            var displayEffect = displayEffects[i];
            if (displayEffect.StatusEffectSO.GetCurrentStack() > 0)
            {
                displayEffect.UpdateSetup();
                return;
            }
            displayEffects.Remove(displayEffect);
            Destroy(displayEffect.gameObject);
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
