using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPanel : MonoBehaviour
{
    [SerializeField] private Transform rewardParent;
    [SerializeField] private GameObject rewardPrefab;

    private Panel _panel;

    private void Awake()
    {
        _panel = GetComponent<Panel>();
    }

    public void Initialize()
    {
        _panel.SetPosition(PanelStates.Show, true);
    }
}
