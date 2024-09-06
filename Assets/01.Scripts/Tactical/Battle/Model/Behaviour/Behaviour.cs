using System;
using System.Collections.Generic;
using System.Linq;

public class Behaviour
{
    public CompareInfo compareInfo;
    public int value;
    public bool onSelf;

    public Behaviour()
    {
        
    }
    
    public bool IsSatisfied(Dictionary<BehaviourInfo, int> from, Dictionary<BehaviourInfo, int> to)
    {
        var fromTotalValue = from.Where(b => b.Key.chargeInDice).Sum(b => b.Value);
        var toTotalValue = to.Where(b => b.Key.chargeInDice).Sum(b => b.Value);
        return IsSatisfied(fromTotalValue, toTotalValue);
    }

    public bool IsSatisfied(int fromValue, int toValue)
    {
        return compareInfo.compareTargetType switch
        {
            CompareTargetType.ConstValue => compareInfo.compareType.IsSatisfied(fromValue, compareInfo.value),
            CompareTargetType.EachOther => compareInfo.compareType.IsSatisfied(fromValue, toValue),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    public virtual int GetValue(int value)
    {
        return value;
    }
}