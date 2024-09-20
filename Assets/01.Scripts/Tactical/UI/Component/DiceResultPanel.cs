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

    public void SetValue(int value)
    {
        DOTween.Complete(this);
        DOTween.To(() => _resultValue, x => _resultValue = x, value, 0.75f).From(0).OnUpdate(() => resultLabel.text = _resultValue.ToString());
    }
}
