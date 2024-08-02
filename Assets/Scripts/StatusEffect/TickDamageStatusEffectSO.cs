using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TickDamageStatusEffectSO", menuName = "Scriptable Object/StatusEffectSO/TickDamageStatusEffectSO")]
public class TickDamageStatusEffectSO : StatusEffectSO
{
    [Header("틱 데미지")]
    [SerializeField] private bool useStack;
    [DrawIf("useStack", false)]
    [SerializeField] private int damage;
    
    public override void UpdateEffect(Creature creature)
    {
        //creature.OnDamage(useStack ? GetCurrentStack() : damage);
    }
}
