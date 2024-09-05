public class AttackBehaviour : Behaviour
{
    public AttackBehaviour(CompareInfo compareInfo, int value, bool onSelf) : base(compareInfo, value, onSelf)
    {
    }

    public override int CalculateValue(int curValue)
    {
        return curValue + Value;
    }
}