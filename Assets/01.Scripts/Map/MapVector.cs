public class MapVector
{
    public readonly int X;
    public readonly int Y;

    public MapVector(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(MapVector other)
    {
        return X == other.X && Y == other.Y;
    }
}
