using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Serialization;

public class RecoveryTextHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text recoveryTMP;
    
    public void Setup(int value)
    {
        recoveryTMP.text = value.ToString();

        var rect = recoveryTMP.rectTransform;
        var sequence = DOTween.Sequence();
        rect.anchoredPosition =
            new Vector2(rect.anchoredPosition.x + Random.Range(-100, 100f), rect.anchoredPosition.y);
        sequence.Append(rect.DOAnchorPosY(750, 1f).SetEase(Ease.OutCubic));
        sequence.Insert(0.7f, recoveryTMP.DOFade(0f, 0.3f).SetEase(Ease.Linear));
        Destroy(gameObject, 1.01f);
    }
}