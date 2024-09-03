using System;

public abstract class Behaviour
{
    public int Value { get; private set; }
    public CompareInfo CompareInfo { get; private set; }
    
    public Behaviour(int value, CompareInfo compareInfo)
    {
        Value = value;
        CompareInfo = compareInfo;
    }

    public virtual bool IsSatisfied(int fromValue, int toValue)
    {
        return CompareInfo.compareTargetType switch
        {
            CompareTargetType.ConstValue => CompareInfo.compareType.OnSatisfied(fromValue, CompareInfo.value),
            CompareTargetType.EachOther => CompareInfo.compareType.OnSatisfied(fromValue, toValue),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}