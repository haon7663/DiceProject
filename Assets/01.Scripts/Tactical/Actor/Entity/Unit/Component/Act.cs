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
    private Animator _animator;
    
    private static readonly int Idle = Animator.StringToHash("Idle");

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _spriteTransform = transform.GetChild(0);
        _spriteRenderer = _spriteTransform.GetComponent<SpriteRenderer>();
        _animator = _spriteTransform.GetComponent<Animator>();
    }

    public void Init()
    {
        _spriteRenderer.sprite = _unit.unitSO.idleSprite;
        _spriteRenderer.DOColor(Color.white, 0.4f).From(Color.black);

        if (_unit.type == UnitType.Player)
            transform.DOMoveX(-1.35f, 0.4f).From(-3f);
    }

    public void DeathAction(AnimationData animationData)
    {
        _spriteRenderer.sprite = animationData.actionSprite;
        _spriteTransform.gameObject.layer = 7;
        _spriteTransform.localPosition = animationData.startOffset;
        _animator.SetBool(Idle, false);

        var sequence = DOTween.Sequence();
        sequence.Append(_spriteTransform.DOLocalMove(animationData.endOffset, 1.2f))
            .JoinCallback(() => CreateEffect(animationData.effectSprite))
            .Join(_spriteRenderer.DOColor(Color.black, 0.9f))
            .Append(_spriteRenderer.DOFade(0, 0.3f))
            .OnComplete(() => Destroy(gameObject));
    }

    public void PerformAction(AnimationData animationData)
    {
        DOTween.Kill(this);
        
        _spriteRenderer.sprite = animationData.actionSprite;
        _spriteRenderer.sortingOrder = animationData.order;
        _spriteTransform.gameObject.layer = 7;
        _spriteTransform.localPosition = animationData.startOffset;
        _animator.SetBool(Idle, false);

        var sequence = DOTween.Sequence();
        sequence.Append(_spriteTransform.DOLocalMove(animationData.endOffset, 1.2f))
            .JoinCallback(() => CreateEffect(animationData.effectSprite));
    }

    public void ResetAction()
    {
        var unitData = _unit.unitSO;
        
        var sequence = DOTween.Sequence();
        sequence.Append(_spriteTransform.DOLocalMove(Vector3.zero, 0.15f).SetEase(Ease.OutSine))
            .AppendCallback(() =>
            {
                _spriteRenderer.sprite = unitData.idleSprite;
                _spriteTransform.gameObject.layer = 0;
                _animator.SetBool(Idle, true);
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
        effectSpriteRenderer.sortingLayerName = "Effect";
        effectSpriteRenderer.sortingOrder = 2;
        effectSpriteRenderer.DOFade(0, 1f).SetEase(Ease.InCirc).OnComplete(() => Destroy(effect));
    }

    public void OnDamage()
    {
        _spriteRenderer.DOColor(Color.white, 0.5f).From(Color.red).SetEase(Ease.OutExpo);
    }
}
