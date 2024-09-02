public class AttackBehaviour : Behaviour
{
    public AttackBehaviour(int value) : base(value)
    {
    }

    public AttackBehaviour(int value, CompareType compareType) : base(value, compareType)
    {
    }
    
    public override int CalculateValue()
    {
        return value;
    }
}