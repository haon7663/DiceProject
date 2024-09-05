using System;

public static class BehaviourExtension
{
    public static Behaviour GetType(this BehaviourType behaviourType)
    {
        switch (behaviourType)
        {
            case BehaviourType.Attack:
                return new AttackBehaviour();
        }
    }
}