using DG.Tweening;
using UnityEngine;

public class AvoidHud : Hud
{
    public override void Initialize(Vector2 pos)
    {
        base.Initialize(pos);
        
        var sequence = DOTween.Sequence();
        sequence.Append(rect.DOAnchorPosY(rect.anchoredPosition.y + 200, 1f));
        sequence.Insert(0.7f, label.DOFade(0f, 0.3f).SetEase(Ease.Linear)).OnComplete(() => Destroy(gameObject));
    }
}