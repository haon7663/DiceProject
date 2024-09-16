using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum GameMode { Battle, Event, Chest, Shop, Boss }

public class GameManager : SingletonDontDestroyOnLoad<GameManager>
{
    public GameMode currentGameMode;

    public float battleSpeed = 1;

    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = battleSpeed;
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