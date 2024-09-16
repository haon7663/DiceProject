using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DefenceHud : Hud
{
    public override void Initialize(Vector2 pos, int value)
    {
        base.Initialize(pos, value);
        
        label.text = $"방어 -{value}";
        
        var sequence = DOTween.Sequence();
        sequence.Append(rect.DOAnchorPosY(rect.anchoredPosition.y + 200, 1f));
        sequence.Insert(0.7f, label.DOFade(0f, 0.3f).SetEase(Ease.Linear)).OnComplete(() => Destroy(gameObject));
    }
}