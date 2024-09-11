using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPanel : MonoBehaviour
{
    [SerializeField] private RewardItem rewardPrefab;
    [SerializeField] private Transform rewardParent;

    private Panel _panel;

    private void Awake()
    {
        _panel = GetComponent<Panel>();
    }

    public void Initialize(List<Reward> rewards)
    {
        _panel.SetPosition(PanelStates.Show, true);

        foreach (var reward in rewards)
        {
            var rewardItem = Instantiate(rewardPrefab, rewardParent);
            rewardItem.Initialize(reward);
        }
    }
}
