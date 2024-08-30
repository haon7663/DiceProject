using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DiceResultPanel : MonoBehaviour
{
    public Unit Unit { get; private set; }

    [SerializeField] private TMP_Text resultLabel;

    private int _resultValue;
    
    public void Initialize(Unit unit)
    {
        Unit = unit;
        _resultValue = 0;
        resultLabel.text = _resultValue.ToString();
    }

    public void AddValue(int value)
    {
        var targetValue = _resultValue + value;
        DOTween.To(() => _resultValue, x => _resultValue = x, targetValue, 0.75f).OnUpdate(() => resultLabel.text = _resultValue.ToString());
    }
}
