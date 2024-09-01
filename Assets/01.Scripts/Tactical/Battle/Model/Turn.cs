using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    public Unit actor;
    public bool isPlayer;

    public void ChangeTurn(Unit unit)
    {
        actor = unit;
        isPlayer = unit.type == UnitType.Player;
    }
}
