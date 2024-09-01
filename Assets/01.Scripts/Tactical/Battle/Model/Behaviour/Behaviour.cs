public abstract class Behaviour
{
    public int value;
    public CompareType compareType;

    public Behaviour()
    {
        
    }
    
    public Behaviour(int value)
    {
        this.value = value;
    }

    public Behaviour(int value, CompareType compareType) : this(value)
    {
        this.compareType = compareType;
    }

    public virtual int CalculateValue()
    {
        return 0;
    }

    public virtual bool CalculateResult(int finalValue)
    {
        return true;
    }
}
