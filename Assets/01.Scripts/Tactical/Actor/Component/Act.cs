using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Act : MonoBehaviour
{
    [SerializeField] private Vector2 defaultPosition;
    [SerializeField] private Vector2 attackStartPosition;
    [SerializeField] private Vector2 attackLastPosition;
    [SerializeField] private Vector2 hitStartPosition;
    [SerializeField] private Vector2 hitLastPosition;
    
    private Unit _unit;
    private SpriteRenderer _spriteRenderer;

    public AnimationCurve curve;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AttackAction()
    {
        var creatureSO = _unit.unitSO;
        
        gameObject.layer = 7;
        transform.position = attackStartPosition;
        _spriteRenderer.sprite = creatureSO.attackSprite;

        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(attackLastPosition, 1.2f));
        sequence.JoinCallback(() =>
        {
            var effect = new GameObject();
            effect.layer = 7;
            effect.transform.SetParent(transform);
            effect.transform.localPosition = Vector3.zero;
            
            var effectSprite = effect.AddComponent<SpriteRenderer>();
            effectSprite.sprite = creatureSO.attackEffectSprite;
            effectSprite.sortingOrder = 2;
            effectSprite.DOFade(0, 1f).SetEase(curve).OnComplete(() => Destroy(effect));
        });
        sequence.AppendCallback(() =>
        {
            _spriteRenderer.sprite = creatureSO.idleSprite;
            gameObject.layer = 1;
        });
        sequence.Append(transform.DOMove(defaultPosition, 0.1f).SetEase(Ease.OutSine));
    }
    
    public void HitAction()
    {
        var creatureSO = _unit.unitSO;
        
        gameObject.layer = 7;
        transform.position = hitStartPosition;
        _spriteRenderer.sprite = _unit.unitSO.hitSprite;

        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(hitLastPosition, 1.2f));
        sequence.JoinCallback(() =>
        {
            var effect = new GameObject();
            effect.layer = 7;
            effect.transform.SetParent(transform);
            effect.transform.localPosition = Vector3.zero;
            
            var effectSprite = effect.AddComponent<SpriteRenderer>();
            effectSprite.sprite = creatureSO.hitEffectSprite;
            effectSprite.sortingOrder = 2;
            effectSprite.DOFade(0, 1f).SetEase(curve).OnComplete(() => Destroy(effect));
        });
        sequence.AppendCallback(() =>
        {
            _spriteRenderer.sprite = _unit.unitSO.idleSprite;
            gameObject.layer = 1;
        });
        sequence.Append(transform.DOMove(defaultPosition, 0.1f).SetEase(Ease.OutSine));
    }
}
