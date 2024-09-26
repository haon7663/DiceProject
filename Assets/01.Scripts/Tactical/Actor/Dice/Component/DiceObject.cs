using System;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DiceObject : MonoBehaviour
{
    [SerializeField] private Renderer diceModel;
    [SerializeField] private TMP_Text countLabel;

    [SerializeField] private Color luckColor;
    [SerializeField] private Color misfortuneColor;
    private int _count;

    public void Initialize(int defaultValue, int addedValue)
    {
        _count = addedValue;
        countLabel.text = defaultValue.ToString();

        var sequence = DOTween.Sequence();
        sequence.AppendInterval(0.95f);
        sequence.Append(DOVirtual.Float(defaultValue, addedValue, 0.25f, f =>
        {
            countLabel.text = Mathf.RoundToInt(f).ToString();
        }).OnComplete(() => countLabel.text = addedValue.ToString()));
        if (defaultValue != addedValue)
            sequence.Join(countLabel.DOColor(defaultValue > addedValue ? misfortuneColor : luckColor, 0.25f));
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

    public void DiceSound()
    {
        SoundManager.Inst.Play("DiceRoll");
    }
}
