using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitFactory
{
    public static GameObject Create(UnitSO unitSO)
    {
        var obj = InstantiatePrefab($"Units/{unitSO.name}");
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
