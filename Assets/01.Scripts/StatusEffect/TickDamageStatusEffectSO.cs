using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TickDamageStatusEffectSO", menuName = "Scriptable Object/StatusEffectSO/TickDamageStatusEffectSO")]
public class TickDamageStatusEffectSO : StatusEffectSO
{
    [Header("틱 데미지")]
    [SerializeField] private bool useStack;
    [DrawIf("useStack", false)]
    [SerializeField] private int value;
    [DrawIf("useStack", true)]
    [SerializeField] private int multiplierValue;
    
    public override void UpdateEffect(Unit unit)
    {
        if (unit.TryGetComponent<Health>(out var health))
        {
            health.OnDamage(GetCurrentValue());
        }
        base.UpdateEffect(unit);
    }
    
    public override string GetDialogString(Unit unit)
    {
        return $"{unit.unitSO.name}은(는) {name}으로(로) {GetCurrentValue()}의 피해를 입었다!";
    }

    public override int GetCurrentValue()
    {
        return useStack ? GetCurrentStack() * multiplierValue : value;
    }
}
