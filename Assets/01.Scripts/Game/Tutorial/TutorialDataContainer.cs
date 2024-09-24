using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class TutorialDataContainer : Singleton<TutorialDataContainer>
{
    public TutorialData tutorialData = new TutorialData(false);
    private static string _tutorialFilePath;

    private void Start()
    {
        _tutorialFilePath = Path.Combine(Application.persistentDataPath, "tutorial.json");
        Load();
    }

    private void Load()
    {
        if (File.Exists(_tutorialFilePath))
        {
            var text = File.ReadAllText(_tutorialFilePath);
            tutorialData = JsonConvert.DeserializeObject<TutorialData>(text);
        }
    }

    public void Save()
    {
        var json = JsonConvert.SerializeObject(tutorialData, Formatting.Indented);
        File.WriteAllText(_tutorialFilePath, json);
    }
}
