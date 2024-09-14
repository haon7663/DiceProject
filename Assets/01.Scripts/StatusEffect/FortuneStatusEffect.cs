using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TickDamageStatusEffectSO", menuName = "Scriptable Object/StatusEffectSO/TickDamageStatusEffectSO")]
public class FortuneStatusEffect : StatusEffectSO
{
    [Header("운")]
    [SerializeField] private int multiplierValue;

    public override int GetCurrentValue()
    {
        return GetCurrentStack() * multiplierValue;
    }
}
