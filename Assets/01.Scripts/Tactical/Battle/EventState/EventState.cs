using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventState : State
{
    protected BattleController owner;
    protected virtual void Awake()
    {
        owner = GetComponent<BattleController>();
    }
}
