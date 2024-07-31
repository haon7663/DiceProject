using System;
using System.IO;
using System.Collections.Generic;
using GDX.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Windows;
using File = System.IO.File;

[Serializable]
public class PlayerData
{
    [Header("주사위")]
    public SerializableDictionary<DiceType, int> dices;
    
    [Header("체력")]
    public int maxHp;
    public int curHp;

    [Header("카드")]
    public List<CardSO> cards;

    [Header("유물")]
    public List<RelicSO> relics;

    public PlayerData(int maxHp, List<CardSO> cards)
    {
        dices = new SerializableDictionary<DiceType, int>()
        {
            { DiceType.Four, 9999999 },
            { DiceType.Six, 12 },
            { DiceType.Eight, 6 },
            { DiceType.Twelve, 2 },
            { DiceType.Twenty, 1 },
        };

        this.maxHp = maxHp;
        curHp = maxHp;
        
        this.cards = cards;

        relics = new List<RelicSO>();
    }
}

public class DataManager : SingletonDontDestroyOnLoad<DataManager>
{
    public PlayerData PlayerData;

    private static string _playerDataFilePath;

    private void Awake()
    {
        _playerDataFilePath = Path.Combine(Application.persistentDataPath, "data.json");
        
        if (File.Exists(_playerDataFilePath))
        {
            var playerDataJson = File.ReadAllText(_playerDataFilePath);
            var playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataJson);

            PlayerData = playerData;
        }
        else
        {
            Generate(GameManager.Inst.player.creatureSO);
        }
    }

    public void Generate(CreatureSO creatureSO)
    {
        var playerData = new PlayerData(creatureSO.hp, creatureSO.cards);
        PlayerData = playerData;
    }
    
    public void Save()
    {
        if (PlayerData == null) return;

        var json = JsonConvert.SerializeObject(PlayerData, Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        
        File.WriteAllText(_playerDataFilePath, json);
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
