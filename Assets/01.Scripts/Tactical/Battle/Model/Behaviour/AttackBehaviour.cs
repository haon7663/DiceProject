public class AttackBehaviour : Behaviour
{
    public int damage;
    
    public AttackBehaviour(int value, int damage) : base(value)
    {
        this.damage = damage;
    }

    public AttackBehaviour(int value, CompareType compareType, int damage) : base(value, compareType)
    {
        this.damage = damage;
    }
    
    public override int CalculateValue()
    {
        return value;
    }
    
    public override void PerformAction(Unit target)
    {
        if (target.TryGetComponent<Health>(out var health))
        {
            health.OnDamage(damage);
        }
    }
}