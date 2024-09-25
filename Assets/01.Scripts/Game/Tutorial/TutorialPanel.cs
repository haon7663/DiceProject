using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TutorialPanel : MonoBehaviour
{
    [Header("Focus Frame")]
    [SerializeField] private RectTransform mask;
    [SerializeField] private RectTransform highlight;
    [SerializeField] private RectTransform labelRect;
    [SerializeField] private Vector2 sizeOffset;

    [Header("Labels")]
    [SerializeField] private TMP_Text titleLabel;
    [SerializeField] private TMP_Text descriptionLabel;

    public void MoveTransform(RectTransform source, bool useDotween = false, float dotweenTime = 0.5f, Ease ease = Ease.Unset)
    {
        var anchoredPosition = source.anchoredPosition;
        var sizeDelta = source.sizeDelta;
        var anchorMin = source.anchorMin;
        var anchorMax = source.anchorMax;
        var pivot = source.pivot;
        
        if (useDotween)
        {
            mask.DOAnchorMin(anchorMin, dotweenTime).SetEase(ease); 
            mask.DOAnchorMax(anchorMax, dotweenTime).SetEase(ease);
            mask.DOPivot(pivot, dotweenTime).SetEase(ease);
            mask.DOAnchorPos(anchoredPosition, dotweenTime).SetEase(ease);
            mask.DOSizeDelta(sizeDelta, dotweenTime).SetEase(ease);
            
            highlight.DOAnchorMin(anchorMin, dotweenTime).SetEase(ease); 
            highlight.DOAnchorMax(anchorMax, dotweenTime).SetEase(ease);
            highlight.DOPivot(pivot, dotweenTime).SetEase(ease);
            highlight.DOAnchorPos(anchoredPosition, dotweenTime).SetEase(ease);
            highlight.DOSizeDelta(sizeDelta + sizeOffset, dotweenTime).SetEase(ease);
        }
        else
        {
            mask.anchorMin = anchorMin;
            mask.anchorMax = anchorMax;
            mask.pivot = pivot;
            mask.anchoredPosition = anchoredPosition;
            mask.sizeDelta = sizeDelta;
            
            highlight.anchorMin = anchorMin;
            highlight.anchorMax = anchorMax;
            highlight.pivot = pivot;
            highlight.anchoredPosition = anchoredPosition;
            highlight.sizeDelta = sizeDelta + sizeOffset;
        }
    }
    
    public void SetLabel(string title, string description)
    {
        titleLabel.text = title;
        descriptionLabel.text = description;
        titleLabel.DOFade(1, 0.15f).From(0);
        descriptionLabel.DOFade(1, 0.15f).From(0);
    }
    
    public IEnumerator SetLabelRect(RectTransform source, bool onTop)
    {
        yield return null;
        
        var bottom = source.anchorMin.y
                     + source.anchoredPosition.y
                     - source.sizeDelta.y * source.pivot.y;

        var top = source.anchorMax.y
                  + source.anchoredPosition.y
                  + source.sizeDelta.y * (1 - source.pivot.y) + 150 + descriptionLabel.rectTransform.sizeDelta.y;
        
        labelRect.anchoredPosition = new Vector2(0, (onTop ? top : bottom));
    }

}