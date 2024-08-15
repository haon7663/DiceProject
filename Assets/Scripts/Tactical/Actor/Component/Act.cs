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
    
    private Creature _creature;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _creature = GetComponent<Creature>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AttackAction()
    {
        gameObject.layer = 7;
        
        transform.position = attackStartPosition;
        _spriteRenderer.sprite = _creature.creatureSO.attackSprite;

        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(attackLastPosition, 1.5f));
        sequence.AppendCallback(() =>
        {
            _spriteRenderer.sprite = _creature.creatureSO.idleSprite;
            gameObject.layer = 1;
        });
        sequence.Append(transform.DOMove(defaultPosition, 0.4f).SetEase(Ease.OutSine));
    }
    
    public void HitAction()
    {
        gameObject.layer = 7;
        
        transform.position = hitStartPosition;
        _spriteRenderer.sprite = _creature.creatureSO.defenceSprite;

        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(hitLastPosition, 1.5f));
        sequence.AppendCallback(() =>
        {
            _spriteRenderer.sprite = _creature.creatureSO.idleSprite;
            gameObject.layer = 1;
        });
        sequence.Append(transform.DOMove(defaultPosition, 0.4f).SetEase(Ease.OutSine));
    }
}
