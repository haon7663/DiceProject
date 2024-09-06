using System;

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
}