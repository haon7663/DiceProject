using System.Linq;
using UnityEngine;

public static class UnitJsonConverter
{
    public static string ToString(this UnitSO unitSO)
    {
        return unitSO.name;
    }
    
    public static UnitSO ToPlayer(this string name)
    {
        var units = Resources.LoadAll<UnitSO>("Units/Player");
        var unit = units.First(unit => unit.name == name);
        if (unit)
            return unit;
        Debug.LogWarning("Unit was null in UnitJsonConverter.ToUnit()");
        return null;
    }
}