public class AvoidBehaviour : Behaviour
{
    public AvoidBehaviour(int value) : base(value)
    {
    }

    public AvoidBehaviour(int value, CompareType compareType) : base(value, compareType)
    {
    }
    
    public override bool CalculateResult(int finalValue)
    {
        return compareType.OnSatisfied(finalValue, value);
    }
}