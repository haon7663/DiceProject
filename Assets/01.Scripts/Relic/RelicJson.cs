using System;
using UnityEngine.Serialization;

[Serializable]
public class RelicJson
{
    public string name;

    public RelicJson(string name)
    {
        this.name = name;
    }
}