using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Act : MonoBehaviour
{
    private Unit _unit;
    private Transform _spriteTransform;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _spriteTransform = transform.GetChild(0);
        _spriteRenderer = _spriteTransform.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = _unit.unitSO.idleSprite;
    }

    public void PerformAction(AnimationData animationData)
    {
        var unitData = _unit.unitSO;
        
        _spriteRenderer.sprite = animationData.actionSprite;
        _spriteTransform.gameObject.layer = 7;
        _spriteTransform.localPosition = animationData.startOffset;

        var sequence = DOTween.Sequence();
        sequence.Append(_spriteTransform.DOLocalMove(animationData.endOffset, 1.2f))
            .JoinCallback(() => CreateEffect(animationData.effectSprite))
            .Append(_spriteTransform.DOLocalMove(Vector3.zero, 0.15f).SetEase(Ease.OutSine))
            .AppendCallback(() =>
            {
                _spriteRenderer.sprite = unitData.idleSprite;
                _spriteTransform.gameObject.layer = 0;
            });
    }

    private void CreateEffect(Sprite effectSprite)
    {
        var effect = new GameObject { layer = 7 };
        effect.transform.SetParent(_spriteTransform);
        effect.transform.localPosition = Vector3.zero;
        effect.transform.localScale = Vector3.one;
        effect.transform.localRotation = Quaternion.identity;

        var effectSpriteRenderer = effect.AddComponent<SpriteRenderer>();
        effectSpriteRenderer.sprite = effectSprite;
        effectSpriteRenderer.sortingOrder = 2;
        effectSpriteRenderer.DOFade(0, 1f).SetEase(Ease.InCirc).OnComplete(() => Destroy(effect));
    }
}
