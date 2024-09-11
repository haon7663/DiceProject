using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text label;

    private Reward _reward;

    public void Initialize(Reward reward)
    {
        _reward = reward;
        
        icon.sprite = reward.GetSprite();
        label.text = reward.GetLabel();
    }

    public void Execute()
    {
        _reward.Execute();
        Destroy(gameObject);
    }
}
