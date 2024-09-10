using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text label;

    public void Initialize(Sprite sprite, string name)
    {
        icon.sprite = sprite;
        label.name = name;
    }
}
