using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInputController : MonoBehaviour
{
    public static event EventHandler<ButtonEventType> ButtonEvent;

    public void SkipAction()
    {
        ButtonEvent?.Invoke(this, ButtonEventType.Skip);
    }
}
