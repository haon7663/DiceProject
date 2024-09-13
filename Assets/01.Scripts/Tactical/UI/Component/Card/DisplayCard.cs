using System;
using DG.Tweening;
using UnityEngine;

public class DisplayCard : Card
{
    private Animator _animator;
    private RectTransform _rect;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rect = GetComponent<RectTransform>();
    }

    public void SetAnim(string trigger)
    {
        _animator.SetTrigger(trigger);
    }
    
    public void MoveTransform(Vector2 pos, bool useDotween = false, float dotweenTime = 0.25f)
    {
        if (useDotween)
            _rect.DOAnchorPos(pos, dotweenTime);
        else
            _rect.anchoredPosition = pos;
    }
}
