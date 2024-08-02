using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TickRecoveryStatusEffectSO", menuName = "Scriptable Object/StatusEffectSO/TickRecoveryStatusEffectSO")]
public class TickRecoveryStatusEffectSO : StatusEffectSO
{
    [Header("틱 회복량")]
    [SerializeField] private bool useStack;
    [DrawIf("useStack", false)]
    [SerializeField] private int recovery;
    
    public override void UpdateEffect(Creature creature)
    {
        //creature.OnRecovery(useStack ? GetCurrentStack() : recovery);
    }
}
