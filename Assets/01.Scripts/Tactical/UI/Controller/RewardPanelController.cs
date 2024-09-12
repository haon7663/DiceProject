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
    
    public void Hide()
    {
        backgroundPanel.SetPosition(PanelStates.Hide, true);
        rewardPanel.panel.SetPosition(PanelStates.Hide, true);
    }
}
