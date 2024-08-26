using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class AvoidTextHandler : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMP_Text avoidTMP;

    public void Start()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOAnchorPosY(750, 1.5f).SetEase(Ease.OutCubic));
        sequence.Insert(0.5f, avoidTMP.DOFade(0, 1f).SetEase(Ease.OutExpo));
    }
}
