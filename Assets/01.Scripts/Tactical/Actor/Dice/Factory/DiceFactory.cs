using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DiceFactory
{
    public static GameObject Create(DiceType diceType)
    {
        var obj = InstantiatePrefab($"Dices/{diceType.ToString()}");
        return obj;
    }
    
    private static GameObject InstantiatePrefab (string name)
    {
        var prefab = Resources.Load<GameObject>(name);
        if (prefab == null) {
            Debug.LogError("No Prefab for name: " + name);
            return new GameObject(name);
        }
        var instance = Object.Instantiate(prefab);
        instance.name = instance.name.Replace("(Clone)", "");

        return instance;
    }
}
