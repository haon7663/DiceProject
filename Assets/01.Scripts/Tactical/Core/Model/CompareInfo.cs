using System;

[Serializable]
public class CompareInfo
{
    public CompareType compareType;

    [DrawIf("compareType", CompareType.Custom)]
    public SpecialCompareType specialCompareType;
    
    [DrawIf("compareType", CompareType.None, true)]
    public CompareTargetType compareTargetType;
    
    [DrawIf("compareType", CompareType.None, true)]
    [DrawIf("compareTargetType", CompareTargetType.ConstValue)]
    public int value;
    
    public bool IsSatisfied(int compareValue)
    {
        return compareType.IsSatisfied(compareValue, value);
    }
    public bool IsSatisfied(int fromValue, int toValue)
    {
        return compareTargetType switch
        {
            CompareTargetType.ConstValue => compareType.IsSatisfied(fromValue, value),
            CompareTargetType.EachOther => compareType.IsSatisfied(fromValue, toValue),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}