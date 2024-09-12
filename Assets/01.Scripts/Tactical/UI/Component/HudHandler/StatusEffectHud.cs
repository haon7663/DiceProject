using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectHud : Hud
{
    [SerializeField] private Image icon;
    
    public override void Initialize(Vector2 pos, int value, Sprite sprite)
    {
        base.Initialize(pos, value, sprite);

        label.text = $"{value}";
        icon.sprite = sprite;
        
        var sequence = DOTween.Sequence();
        sequence.Append(rect.DOAnchorPosY(rect.anchoredPosition.y + 200, 1f));
        sequence.Insert(0.7f, label.DOFade(0f, 0.3f).SetEase(Ease.Linear)).OnComplete(() => Destroy(gameObject));
    }
}