using System;

public abstract class Behaviour
{
    public CompareInfo CompareInfo { get; private set; }
    public int Value { get; private set; }
    public bool OnSelf { get; private set; }
    
    public Behaviour(CompareInfo compareInfo, int value, bool onSelf)
    {
        CompareInfo = compareInfo;
        Value = value;
        OnSelf = onSelf;
    }

    public bool IsSatisfied(int fromValue, int toValue)
    {
        return CompareInfo.compareTargetType switch
        {
            CompareTargetType.ConstValue => CompareInfo.compareType.IsSatisfied(fromValue, CompareInfo.value),
            CompareTargetType.EachOther => CompareInfo.compareType.IsSatisfied(fromValue, toValue),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    public virtual int CalculateValue(int curValue)
    {
        return curValue;
    }
}