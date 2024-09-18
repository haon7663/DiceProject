using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class RewardCard : Card, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public event Action OnClick;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        DOTween.Kill(transform);
        transform.DOScale(Vector2.one * 1.2f, 0.25f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.Kill(transform);
        transform.DOScale(Vector2.one, 0.25f);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        DataManager.Inst.playerData.Cards.Add(Data.ToJson());
        OnClick?.Invoke();
    }
}
