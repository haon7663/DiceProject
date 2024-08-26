using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [Header("Battle Objects")]
    public GameObject[] battleObjects;

    [Header("Event Objects")]
    public GameObject[] eventObjects;

    [Header("Shop Objects")]
    public GameObject[] shopObjects;

    [Header("Boss Objects")]
    public GameObject[] bossObjects;
    
    public void GameSet(GameMode mode)
    {
        DeactivateAllObjects();
        
        switch (mode)
        {
            case GameMode.Battle:
                ActivateObjects(battleObjects);
                break;
            case GameMode.Event:
                ActivateObjects(eventObjects);
                break;
            case GameMode.Shop:
                ActivateObjects(shopObjects);
                break;
            case GameMode.Boss:
                ActivateObjects(bossObjects);
                break;
        }
    }

    private void DeactivateAllObjects()
    {
        DeactivateObjects(battleObjects);
        DeactivateObjects(eventObjects);
        DeactivateObjects(shopObjects);
        DeactivateObjects(bossObjects);
    }

    private void ActivateObjects(GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            obj.SetActive(true);
        }
    }

    private void DeactivateObjects(GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            obj.SetActive(false);
        }
    }
}
