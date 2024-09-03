using System;
using UnityEngine;

public static class Compare
{
    public static bool IsSatisfied(this CompareType compareType, float primaryValue, float secondaryValue)
    {
        switch (compareType)
        {
            case CompareType.None:
                return true;
            case CompareType.Greater:
                if (primaryValue > secondaryValue)
                    return true;
                break;
            case CompareType.GreaterOrEqual:
                if (primaryValue >= secondaryValue)
                    return true;
                break;
            case CompareType.Less:
                if (primaryValue < secondaryValue)
                    return true;
                break;
            case CompareType.LessOrEqual:
                if (primaryValue <= secondaryValue)
                    return true;
                break;
            case CompareType.Equal:
                if (Mathf.Approximately(primaryValue, secondaryValue))
                    return true;
                break;
            case CompareType.Custom:
                return true;
            default:
                throw new ArgumentOutOfRangeException(nameof(compareType), compareType, null);
        }
        return false;
    }
}