using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : Singleton<RelicManager>
{
    [SerializeField] private Transform relicLayout;
    [SerializeField] private RelicIcon relicPrefab;

    public void AddRelic(RelicSO relicSO)
    {
        var newRelic = Instantiate(relicPrefab, relicLayout);
        newRelic.Setup(relicSO);
    }
}
