using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageTextHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text damageTMP;
    
    public void Setup(int value)
    {
        damageTMP.text = "-" + value.ToString();

        var rect = damageTMP.rectTransform;
        var sequence = DOTween.Sequence();
        sequence.Append(rect.DOAnchorPosY(0, 1f).SetEase(Ease.InBack));
        sequence.Insert(0, rect.DOAnchorPosX(rect.anchoredPosition.x + Random.Range(-200f, 200f), 1f).SetEase(Ease.Linear));
        sequence.Insert(0.7f, damageTMP.DOFade(0f, 0.3f).SetEase(Ease.Linear));
        Destroy(gameObject, 1.01f);
    }
}