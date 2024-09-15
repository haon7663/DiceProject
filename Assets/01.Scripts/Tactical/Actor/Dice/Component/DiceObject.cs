using System;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DiceObject : MonoBehaviour
{
    [SerializeField] private TMP_Text countLabel;

    public void Initialize(int count)
    {
        countLabel.text = count.ToString();
    }
}
