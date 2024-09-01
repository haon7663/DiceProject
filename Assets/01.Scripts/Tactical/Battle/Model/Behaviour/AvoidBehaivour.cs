public class AvoidBehaivour : Behaviour
{
    public override bool CalculateResult(int finalValue)
    {
        return compareType.OnSatisfied(finalValue, value);
    }
}