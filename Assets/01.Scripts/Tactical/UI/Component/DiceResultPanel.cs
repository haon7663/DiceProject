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

    public void SetValue(int value, bool useDotween = false, float dotweenTime = 0.5f)
    {
        DOTween.Complete(this);

        if (useDotween)
            DOTween.To(() => _resultValue, x => _resultValue = x, value, dotweenTime).From(0).OnUpdate(() => resultLabel.text = _resultValue.ToString());
        else
        {
            _resultValue = value;
            resultLabel.text = value.ToString();
        }
    }
    
    public void AddValue(int value)
    {
        //DOTween.To(() => _resultValue, x => _resultValue = x, _resultValue + value, 0.3f).From(0).OnUpdate(() => resultLabel.text = _resultValue.ToString()).SetEase(Ease.OutQuint);

        var targetSize = Mathf.Clamp01(_resultValue / 12f) * 0.5f + 1.4f;
        
        _resultValue += value;
        resultLabel.DOKill();
        resultLabel.DOScale(1, 0.4f).From(targetSize).SetEase(Ease.InQuad);

        resultLabel.text = _resultValue.ToString();
    }
}
