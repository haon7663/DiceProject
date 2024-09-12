using DG.Tweening;
using UnityEngine;

public class StatusEffectHud : Hud
{
    public override void Initialize(Vector2 pos, int value, Sprite sprite)
    {
        base.Initialize(pos, value, sprite);

        label.text = $"{value}";
        
        var sequence = DOTween.Sequence();
        sequence.Append(rect.DOAnchorPosY(rect.anchoredPosition.y + 200, 1f));
        sequence.Insert(0.7f, label.DOFade(0f, 0.3f).SetEase(Ease.Linear)).OnComplete(() => Destroy(gameObject));
    }
}