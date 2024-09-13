using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum GameMode { Battle, Event, Shop, Boss }

public class GameManager : SingletonDontDestroyOnLoad<GameManager>
{
    public Unit player;
    public GameMode currentGameMode;
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