using System;
using TMPro;
using UnityEngine;

public class GoldPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text countLabel;

    private void Start()
    {
        UpdateGold();
    }

    public void UpdateGold()
    {
        countLabel.text = DataManager.Inst.playerData.Gold.ToString();
    }
}