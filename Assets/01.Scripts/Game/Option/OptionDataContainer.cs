using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class OptionDataContainer : Singleton<OptionDataContainer>
{
    public SoundSettingsData soundSettingsData = new SoundSettingsData(0.6f, 0.6f);
    private static string _soundSettingsFilePath;

    private void Start()
    {
        _soundSettingsFilePath = Path.Combine(Application.persistentDataPath, "settings-sound.json");
        Load();
    }

    private void Load()
    {
        if (File.Exists(_soundSettingsFilePath))
        {
            var text = File.ReadAllText(_soundSettingsFilePath);
            soundSettingsData = JsonConvert.DeserializeObject<SoundSettingsData>(text);
        }
    }

    public void Save()
    {
        var json = JsonConvert.SerializeObject(soundSettingsData, Formatting.Indented);
        File.WriteAllText(_soundSettingsFilePath, json);
    }
}
