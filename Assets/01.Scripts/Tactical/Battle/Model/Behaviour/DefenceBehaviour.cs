    public class DefenceBehaviour : Behaviour
    {
        public override int GetValue(int value)
        {
            return value - this.value;
        }
    }