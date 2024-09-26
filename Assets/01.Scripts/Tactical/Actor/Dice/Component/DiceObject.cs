using System;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DiceObject : MonoBehaviour
{
    [SerializeField] private Renderer diceModel;
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

    public void Dissolve()
    {
        Material materialInstance = diceModel.material;
        materialInstance.DOFloat(1.0f, "_step", 0.375f).SetEase(Ease.InCirc);
        countLabel.DOFade(0, 0.5f).SetEase(Ease.InCirc);
    }
}
