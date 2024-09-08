using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TickRecoveryStatusEffectSO", menuName = "Scriptable Object/StatusEffectSO/TickRecoveryStatusEffectSO")]
public class TickRecoveryStatusEffectSO : StatusEffectSO
{
    [Header("틱 회복량")]
    [SerializeField] private bool useStack;
    [DrawIf("useStack", false)]
    [SerializeField] private int value;
    [DrawIf("useStack", true)]
    [SerializeField] private int multiplierValue;
    
    public override void UpdateEffect(Unit unit)
    {
        if (!unit.TryGetComponent<Health>(out var health))
            return;
        
        health.OnRecovery(GetCurrentValue());
    }
    
    public override int GetCurrentValue()
    {
        return useStack ? GetCurrentStack() * multiplierValue : value;
    }
}
