using System;
using System.IO;
using System.Collections.Generic;
using GDX.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;
using File = System.IO.File;
using Map;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerData
{
    public string name;
    private int _maxHealth;
    private int _health;
    private int _gold;
    private SerializableDictionary<DiceType, int> _dices;
    private List<CardJson> _cards;
    private string[] _items;
    private List<RelicJson> _relics;
    
    public event Action<string, object> OnValueChanged;

    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            if (_maxHealth == value) return;
            _maxHealth = value;
            OnValueChanged?.Invoke("Health", _maxHealth);
        }
    }
    public int Health
    {
        get => _health;
        set => _health = value;
    }
    
    public int Gold
    {
        get => _gold;
        set
        {
            if (_gold == value) return;
            _gold = value;
            OnValueChanged?.Invoke("Gold", _gold);
        }
    }
    public SerializableDictionary<DiceType, int> Dices
    {
        get => _dices;
        set => _dices = value;
    }

    public List<CardJson> Cards
    {
        get => _cards;
        set
        {
            if (_cards == value) return;
            _cards = value;
            OnValueChanged?.Invoke("Cards", _cards);
        }
    }

    public string[] Items
    {
        get => _items;
        set
        {
            if (_items == value) return;
            _items = value;
            OnValueChanged?.Invoke("Items", _items);
        }
    }

    public List<RelicJson> Relics
    {
        get => _relics;
        set
        {
            if (_relics == value) return;
            _relics = value;
            OnValueChanged?.Invoke("Relics", _relics);
        }
    }

    public Map.Map map;
    
    public PlayerData(string creatureName, int hp, int gold, SerializableDictionary<DiceType, int> dices, List<CardJson> cards, string[] items, List<RelicJson> relics, Map.Map map)
    {
        name = creatureName;
        _maxHealth = _health = hp;
        _gold = gold;
        _dices = dices;
        _cards = cards;
        _items = items;
        _relics = relics;

        this.map = map;
    }
}

public class DataManager : SingletonDontDestroyOnLoad<DataManager>
{
    public PlayerData playerData;

    private static string _playerDataFilePath;

    protected override void Awake()
    {
        base.Awake();
        
        _playerDataFilePath = Path.Combine(Application.persistentDataPath, "data.json");
        
        if (File.Exists(_playerDataFilePath))
        {
            var playerDataJson = File.ReadAllText(_playerDataFilePath);
            var playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataJson);

            this.playerData = playerData;

            if (SceneManager.GetActiveScene().name == "CharacterSelection")
                SceneManager.LoadScene("Battle");
        }
    }

    private void OnEnable()
    {
        playerData.OnValueChanged += HandleValueChanged;
    }
    
    private void OnDisable()
    {
        playerData.OnValueChanged -= HandleValueChanged;
    }

    private void HandleValueChanged(string key, object value)
    {
        switch (key)
        {
            case "Gold":
                print("Gold");
                BattleController.Inst.goldPanelController.UpdateGold();
                break;
            case "Dices":
                print("Dices");
                BattleController.Inst.diceCountPanelController.UpdateCount();
                break;
            case "Cards":
                print("Cards");
                BattleController.Inst.interactionCardController.InitDeck();
                break;
            case "Health":
                print("Health");
                if ((int)value > 0)
                    BattleController.Inst.player.GetComponent<Health>().OnDamage((int)value);
                else
                    BattleController.Inst.player.GetComponent<Health>().OnRecovery(-(int)value);
                break;
        }
    }

    public void AddHealth(int value)
    { 
        playerData.Health += value;
        playerData.Health = Mathf.Clamp(playerData.Health, 0, playerData.MaxHealth);
        
        if (value > 0)
            BattleController.Inst.player.GetComponent<Health>().OnRecovery(value);
        else
            BattleController.Inst.player.GetComponent<Health>().OnDamage(-value);
    }
    
    public void AddDices(DiceType diceType, int addValue)
    {
        playerData.Dices[diceType] += addValue;
        BattleController.Inst.diceCountPanelController.AddCount(diceType, addValue);
    }

    public void Generate(UnitSO unitData)
    {
        var item = new string[8]
        {
            "활력의 부적", "행운의 부적", "", "", "", "", "", ""
        };
        
        var playerData = new PlayerData(unitData.name, unitData.maxHp, 100, new SerializableDictionary<DiceType, int>()
        {
            { DiceType.Four, 9999999 },
            { DiceType.Six, 6 },
            { DiceType.Eight, 4 },
            { DiceType.Twelve, 1 },
            { DiceType.Twenty, 0 },
        }, unitData.cards.ToJson(), item, new List<RelicJson>() { unitData.startRelic.ToJson() }, MapGenerator.GetMap(Resources.Load<MapConfig>("MapConfigs/MapConfig")));
        
        this.playerData = playerData;
    }
    
    public void Save()
    {
        if (playerData == null) return;

        var json = JsonConvert.SerializeObject(playerData, Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        
        File.WriteAllText(_playerDataFilePath, json);
    }

    public void Delete()
    {
        File.Delete(_playerDataFilePath);
    }

    private void OnApplicationQuit()
    {
        #if UNITY_EDITOR
        //Save();
        #endif
    }
}
