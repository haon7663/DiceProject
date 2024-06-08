using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class StatusEffectTextHandler : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMP_Text statusEffectTMP;

    public void SetUp(StatusEffectSO statusEffectSO, int value)
    {
        statusEffectTMP.text = statusEffectSO.label + " +" + value;
        
        var sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOAnchorPosY(750, 1.5f).SetEase(Ease.OutCubic));
        sequence.Insert(0.5f, statusEffectTMP.DOFade(0, 1f).SetEase(Ease.OutExpo));
    }
}
