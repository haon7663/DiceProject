using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPanel : MonoBehaviour
{
    public Panel panel;
    
    [SerializeField] private RewardItem rewardPrefab;
    [SerializeField] private Transform rewardParent;
    
    public void Initialize(List<Reward> rewards)
    {
        panel.SetPosition(PanelStates.Show, true);

        foreach (var reward in rewards)
        {
            var rewardItem = Instantiate(rewardPrefab, rewardParent);
            rewardItem.Initialize(reward);
        }
    }
}
