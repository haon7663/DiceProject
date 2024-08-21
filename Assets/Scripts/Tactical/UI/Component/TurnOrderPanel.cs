using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TurnOrderPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text orderLabel;

    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    public IEnumerator Show(string text)
    {
        orderLabel.text = text;

        _rect.anchoredPosition = new Vector2(-1280, _rect.anchoredPosition.y);
        var sequence = DOTween.Sequence();
        sequence.Append(_rect.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutQuint));
        sequence.Append(_rect.DOAnchorPosX(1280f, 0.5f).SetEase(Ease.InQuint));

        yield return sequence.WaitForCompletion();
    }
}
