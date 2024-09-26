using System;
using UnityEngine.Serialization;

public enum ConditionType { Health }

[Serializable]
public class SkillCondition
{
    public ConditionType conditionType;
    public CompareType compareType;
    public int compareValue;

    public bool IsSatisfied(Unit unit)
    {
        switch (conditionType)
        {
            case ConditionType.Health:
                if (unit.TryGetComponent<Health>(out var health))
                {
                    return compareType.IsSatisfied(health.curHp, compareValue);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return false;
    }
}