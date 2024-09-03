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
}