public class DefenceBehaviour : Behaviour
{
    public DefenceBehaviour(int value) : base(value)
    {
    }

    public DefenceBehaviour(int value, CompareType compareType) : base(value, compareType)
    {
    }
    
    public override int CalculateValue()
    {
        return -value;
    }
}