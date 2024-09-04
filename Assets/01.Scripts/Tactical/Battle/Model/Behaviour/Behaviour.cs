using System;

public class Behaviour
{
    public BehaviourType BehaviourType { get; private set; }
    public CompareInfo CompareInfo { get; private set; }
    public int Value { get; private set; }
    public bool OnSelf { get; private set; }
    
    public Behaviour(CompareInfo compareInfo, BehaviourType behaviourType, int value, bool onSelf)
    {
        BehaviourType = behaviourType;
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
}