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
    
    public static GameObject InstantiatePrefab (string name)
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
    
    public static DiceObject RollingDice(this DiceType diceType, Vector3 pos)
    {
        var dice = Create(diceType);
        dice.transform.position = pos;
        dice.transform.rotation = Quaternion.Euler(0, Random.Range(-15f, 15f), 0);

        var value = diceType.GetDiceValue();
        var diceObject = dice.GetComponent<DiceObject>();
        diceObject.Initialize(value);

        return diceObject;
    }
    
    public static Vector3 CalculateDicePosition(int index, int maxIndex)
    {
        var defaultPos = new Vector3(2.5f * (index - (float)(maxIndex - 1) / 2), -2.5f);
        var randPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * 0.4f;
        return defaultPos + randPos;
    }
}
