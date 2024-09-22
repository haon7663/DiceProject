using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayCard : Card, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform _rect;

    public Vector2 originPos;
    public Vector2 originScale;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }
    
    public void MoveTransform(Vector2 pos, bool useDotween = false, float dotweenTime = 0.25f, Ease ease = Ease.Unset)
    {
        if (useDotween)
            _rect.DOAnchorPos(pos, dotweenTime).SetEase(ease);
        else
            _rect.anchoredPosition = pos;
    }
    
    public void ChangeScale(Vector2 scale, bool useDotween = false, float dotweenTime = 0.25f, Ease ease = Ease.Unset)
    {
        if (useDotween)
            transform.DOScale(scale, dotweenTime).SetEase(ease);
        else
            transform.localScale = scale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MoveTransform(new Vector2(0, 1600), true, 0.2f, Ease.OutCirc);
        ChangeScale(Vector2.one * 1.3f, true, 0.2f, Ease.OutCirc);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MoveTransform(originPos, true, 0.2f, Ease.OutCirc);
        ChangeScale(originScale, true, 0.2f, Ease.OutCirc);
    }
}
