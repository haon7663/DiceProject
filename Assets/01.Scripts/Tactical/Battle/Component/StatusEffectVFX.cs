using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StatusEffectVFX : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    public void Initialize(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.DOFade(0, 0.65f).SetEase(Ease.InCirc).OnComplete(() => Destroy(gameObject));
    }
}
