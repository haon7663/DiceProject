using System;
using UnityEngine;

public class EventStageSO : ScriptableObject
{
}

[Serializable]
public class EventOption
{
    [TextArea] public string description;
}