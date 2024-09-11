using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPanelController : MonoBehaviour
{
    [SerializeField] private Panel backgroundPanel;
    [SerializeField] private RewardPanel rewardPanel;

    public void Show(List<Reward> rewards)
    {
        backgroundPanel.SetPosition(PanelStates.Show, true);
        rewardPanel.Initialize(rewards);
    }
}
