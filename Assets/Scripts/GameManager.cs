using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum GameMode { Battle, Event, Shop, Boss }
public class GameManager : SingletonDontDestroyOnLoad<GameManager>
{
    public Creature player;
    public Creature enemy;
    
    public GameMode currentGameMode;

    [Header("Battle Objects")]
    public GameObject[] battleObjects;

    [Header("Event Objects")]
    public GameObject[] eventObjects;

    [Header("Shop Objects")]
    public GameObject[] shopObjects;

    [Header("Boss Objects")]
    public GameObject[] bossObjects;

    private void Awake()
    {
        SetGameMode(currentGameMode);
    }
    
    public void SetGameMode(GameMode mode)
    {
        currentGameMode = mode;
        
        DeactivateAllObjects();
        
        switch (currentGameMode)
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

public static class YieldInstructionCache
{
    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        if (!waitForSeconds.TryGetValue(seconds, out var wfs))
            waitForSeconds.Add(seconds, wfs = new WaitForSeconds(seconds));
        return wfs;
    }
}