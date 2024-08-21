using UnityEngine;

public enum Drivers {
    None,
    Human,
    Computer
}

public class Driver : MonoBehaviour
{
    public Drivers normal;
    public Drivers special;

    public Drivers Current => special == Drivers.None ? normal : special;
}