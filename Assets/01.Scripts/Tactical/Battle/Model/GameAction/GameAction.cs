using System;
using UnityEngine;

public abstract class GameAction : ScriptableObject
{
    public abstract void Execute();

    public abstract string GetDialog();

    public string AddColor(string log, string color = "yellow")
    {
        return string.Concat($"<color={color}>", log, "</color>");
    }
}
