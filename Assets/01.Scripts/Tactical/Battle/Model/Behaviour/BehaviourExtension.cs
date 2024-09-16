using System;
using System.Collections.Generic;
using System.Linq;

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
            _ => throw new ArgumentOutOfRangeException(nameof(behaviourType), behaviourType, null)
        };
    }
    
    public static List<Behaviour> CreateBehaviours(Unit unit)
    {
        var behaviours = new List<Behaviour>();
        foreach (var (behaviourInfo, value) in unit.behaviourValues)
        {
            var behaviour = (Behaviour)Activator.CreateInstance(behaviourInfo.behaviourType.GetBehaviourClass());
            behaviour.compareInfo = behaviourInfo.compareInfo;
            behaviour.onSelf = behaviourInfo.onSelf;
            behaviour.value = value + behaviourInfo.basicValue;
            if (behaviour is StatusEffectBehaviour statusEffectBehaviour)
            {
                statusEffectBehaviour.statusEffectSO = behaviourInfo.statusEffectSO;
            }
            behaviours.Add(behaviour);
        }

        if (unit.TryGetComponent<Relic>(out var relic))
        {
            foreach (var relicSO in relic.relics)
            {
                var behaviourInfo = relicSO.behaviourInfo;
                var behaviour = (Behaviour)Activator.CreateInstance(relicSO.behaviourInfo.behaviourType.GetBehaviourClass());
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
        var fromBehaviours = CreateBehaviours(from);
        var toBehaviours = CreateBehaviours(to);
        
        return fromBehaviours.Where(b => b.GetType() == behaviourType.GetBehaviourClass())
                   .Any(b => !b.onSelf && b.IsSatisfied(from.behaviourValues, to.behaviourValues)) ||
               toBehaviours.Where(b => b.GetType() == behaviourType.GetBehaviourClass())
                   .Any(b => b.onSelf && b.IsSatisfied(to.behaviourValues, from.behaviourValues));
    }
    
    public static int GetSatisfiedBehavioursSum(this BehaviourType behaviourType, Unit from, Unit to)
    {
        var fromBehaviours = CreateBehaviours(from);
        var toBehaviours = CreateBehaviours(to);
        
        return fromBehaviours.Where(b => b.GetType() == behaviourType.GetBehaviourClass())
                   .Where(b => !b.onSelf && b.IsSatisfied(from.behaviourValues, to.behaviourValues)).Sum(b => b.value) +
               toBehaviours.Where(b => b.GetType() == behaviourType.GetBehaviourClass())
                   .Where(b => b.onSelf && b.IsSatisfied(to.behaviourValues, from.behaviourValues)).Sum(b => b.value);
    }
}