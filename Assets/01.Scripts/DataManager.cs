using System;
using System.IO;
using System.Collections.Generic;
using GDX.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;
using File = System.IO.File;

[Serializable]
public class PlayerData
{
    [Header("기본 정보")]
    public string name;
    
    [Header("체력")]
    public int maxHp;
    public int curHp;

    [Header("자원")]
    public int gold;
    public SerializableDictionary<DiceType, int> dices;
    
    [Header("카드")]
    public List<CardJson> cards;

    [Header("유물")]
    public List<RelicJson> relics;

    public PlayerData(string creatureName, int maxHp, List<CardJson> cards, List<RelicJson> relics)
    {
        name = creatureName;
        
        dices = new SerializableDictionary<DiceType, int>()
        {
            { DiceType.Four, 9999999 },
            { DiceType.Six, 12 },
            { DiceType.Eight, 6 },
            { DiceType.Twelve, 24 },
            { DiceType.Twenty, 1 },
        };

        this.maxHp = maxHp;
        curHp = maxHp;
        
        this.cards = cards;
        this.relics = relics;
    }
}

public class DataManager : SingletonDontDestroyOnLoad<DataManager>
{
    public PlayerData playerData;

    private static string _playerDataFilePath;

    private void Awake()
    {
        _playerDataFilePath = Path.Combine(Application.persistentDataPath, "data.json");
        
        if (File.Exists(_playerDataFilePath))
        {
            var playerDataJson = File.ReadAllText(_playerDataFilePath);
            var playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataJson);

            this.playerData = playerData;
        }
        else
        {
            Generate(GameManager.Inst.player.unitSO);
        }
    }

    public void Generate(UnitSO unitSO)
    {
        var playerData = new PlayerData(unitSO.name, unitSO.maxHp, unitSO.cards.ToJson(), new List<RelicJson>());
        this.playerData = playerData;
    }
    
    public void Save()
    {
        if (playerData == null) return;

        var json = JsonConvert.SerializeObject(playerData, Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        
        File.WriteAllText(_playerDataFilePath, json);
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
