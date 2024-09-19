using System;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DiceObject : MonoBehaviour
{
    [SerializeField] private TMP_Text countLabel;
    private int _count;

    public void Initialize(int count)
    {
        _count = count;
        countLabel.text = count.ToString();
    }

    public int GetValue()
    {
        return _count;
    }
}
