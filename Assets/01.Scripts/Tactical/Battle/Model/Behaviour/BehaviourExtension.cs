using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class BehaviourExtension
{
    public static Type GetBehaviourClass(this BehaviourType behaviourType)
    {
        return behaviourType switch
        {
            BehaviourType.Attack => typeof(AttackBehaviour),
            BehaviourType.Defence => typeof(DefenceBehaviour),
            BehaviourType.Avoid => typeof(AvoidBehaviour),
            BehaviourType.Counter => typeof(CounterBehaviour),
            BehaviourType.StatusEffect => typeof(StatusEffectBehaviour),
            BehaviourType.Recovery => typeof(RecoveryBehaviour),
            _ => throw new ArgumentOutOfRangeException(nameof(behaviourType), behaviourType, null)
        };
    }
    
    public static List<Behaviour> CreateBehaviours(this List<BehaviourInfo> behaviourInfos)
    {
        var behaviours = new List<Behaviour>();
        foreach (var behaviourInfo in behaviourInfos)
        {
            var behaviour = (Behaviour)Activator.CreateInstance(behaviourInfo.behaviourType.GetBehaviourClass());
            behaviour.compareInfo = behaviourInfo.compareInfo;
            behaviour.onSelf = behaviourInfo.onSelf;
            behaviour.value =  behaviourInfo.basicValue;
            if (behaviour is StatusEffectBehaviour statusEffectBehaviour)
            {
                statusEffectBehaviour.statusEffectSO = behaviourInfo.statusEffectSO;
            }
            behaviours.Add(behaviour);
        }
        
        return behaviours;
    }
    
    public static List<Behaviour> CreateBehaviours(Unit from, Unit to)
    {
        var behaviours = new List<Behaviour>();
        
        behaviours.AddRange(CreateUnitBehaviours(from));

        behaviours.AddRange(CreateRelicBehaviour(from, to));
        
        return behaviours;
    }
    
    private static List<Behaviour> CreateUnitBehaviours(Unit from)
    {
        var behaviours = new List<Behaviour>();
        foreach (var (behaviourInfo, value) in from.behaviourValues)
        {
            var behaviour = (Behaviour)Activator.CreateInstance(behaviourInfo.behaviourType.GetBehaviourClass());
            behaviour.compareInfo = behaviourInfo.compareInfo;
            behaviour.onSelf = behaviourInfo.onSelf;
            behaviour.value = behaviourInfo.basicValue + value;
            if (behaviour is StatusEffectBehaviour statusEffectBehaviour)
            {
                statusEffectBehaviour.statusEffectSO = behaviourInfo.statusEffectSO;
            }
            behaviours.Add(behaviour);
        }
        
        return behaviours;
    }
    
    private static List<Behaviour> CreateRelicBehaviour(Unit from, Unit to)
    {
        var behaviours = new List<Behaviour>();
        if (from.TryGetComponent<Relic>(out var relic))
        {
            var behaviourInfos = new List<BehaviourInfo>();
            
            behaviourInfos.AddRange(relic.GetInfosAndExecute(RelicActiveType.AfterAction));
            if (BehaviourType.Attack.IsSatisfiedBehaviours(from, to))
            {
                behaviourInfos.AddRange(relic.GetInfosAndExecute(RelicActiveType.AfterAttack));
            }
            if (BehaviourType.Attack.IsSatisfiedBehaviours(to, from))
            {
                behaviourInfos.AddRange(relic.GetInfosAndExecute(RelicActiveType.AfterHit));
            }
            if (BehaviourType.StatusEffect.IsSatisfiedBehaviours(to, from))
            {
                behaviourInfos.AddRange(relic.GetInfosAndExecute(RelicActiveType.AfterGetStatusEffect));
            }
            
            foreach (var behaviourInfo in behaviourInfos)
            {
                var behaviour = (Behaviour)Activator.CreateInstance(behaviourInfo.behaviourType.GetBehaviourClass());
                behaviour.compareInfo = behaviourInfo.compareInfo;
                behaviour.onSelf = behaviourInfo.onSelf;
                behaviour.value = behaviourInfo.basicValue;
                if (behaviour is StatusEffectBehaviour statusEffectBehaviour)
                {
                    statusEffectBehaviour.statusEffectSO = behaviourInfo.statusEffectSO;
                }
                behaviours.Add(behaviour);
            }
        }
        return behaviours;
    }
    
    public static bool IsSatisfiedBehaviours(this BehaviourType behaviourType, Unit from, Unit to)
    {
        var fromBehaviours = CreateUnitBehaviours(from);
        var toBehaviours = CreateUnitBehaviours(to);
        
        return fromBehaviours.Where(b => b.GetType() == behaviourType.GetBehaviourClass())
                   .Any(b => !b.onSelf && b.IsSatisfied(from.behaviourValues, to.behaviourValues)) ||
               toBehaviours.Where(b => b.GetType() == behaviourType.GetBehaviourClass())
                   .Any(b => b.onSelf && b.IsSatisfied(to.behaviourValues, from.behaviourValues));
    }
    
    public static int GetSatisfiedBehavioursSum(this BehaviourType behaviourType, Unit from, Unit to)
    {
        var fromBehaviours = CreateUnitBehaviours(from);
        var toBehaviours = CreateUnitBehaviours(to);
        
        return fromBehaviours.Where(b => b.GetType() == behaviourType.GetBehaviourClass())
                   .Where(b => !b.onSelf && b.IsSatisfied(from.behaviourValues, to.behaviourValues)).Sum(b => b.value) +
               toBehaviours.Where(b => b.GetType() == behaviourType.GetBehaviourClass())
                   .Where(b => b.onSelf && b.IsSatisfied(to.behaviourValues, from.behaviourValues)).Sum(b => b.value);
    }
}