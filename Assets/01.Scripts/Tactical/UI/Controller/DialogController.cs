using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private Dialog dialogPrefab;
    
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private Image underMark;

    private List<Dialog> _dialogs = new List<Dialog>();

    private bool _isShown;
    
    [ContextMenu("GenerateDialog")]
    public void GenerateDialog()
    {
        var dialog = Instantiate(dialogPrefab, contentRect.transform);
        _dialogs.Add(dialog);
        
        SetParentPosition(dialog);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GenerateDialog();
    }

    public void UpdateDialogTransparency()
    {
        foreach (var dialog in _dialogs)
        {
            var dialogPos = scrollRect.transform.InverseTransformPoint(dialog.transform.position);

            var alpha = dialogPos.y switch
            {
                < -65f => Mathf.Clamp01(1 - (Mathf.Abs(dialogPos.y) - 65f) / 50f),
                > 35f => Mathf.Clamp01(1 - (dialogPos.y - 35f) / 50f),
                _ => 1f
            };

            dialog.SetTrasnparency(alpha);
        }
    }

    public void SetUnderMark()
    {
        if (_dialogs.Count < 2) return;
        
        if (scrollRect.transform.InverseTransformPoint(_dialogs.Last().transform.position).y < -65f && !_isShown)
        {
            DOTween.Kill(underMark);
            _isShown = true;
            underMark.DOFade(1, 0.25f);
        }
        else if (scrollRect.transform.InverseTransformPoint(_dialogs.Last().transform.position).y >= -65f && _isShown)
        {
            DOTween.Kill(underMark);
            _isShown = false;
            underMark.DOFade(0, 0.25f);
        }
    }

    private void SetParentPosition(Dialog dialog)
    {
        DOTween.Kill(contentRect);
        Canvas.ForceUpdateCanvases();
        
        Vector2 contentPos = scrollRect.transform.InverseTransformPoint(contentRect.position);
        Vector2 dialogPos = scrollRect.transform.InverseTransformPoint(dialog.transform.position);
        Vector2 offset = contentPos - dialogPos;
        
        contentRect.DOAnchorPosY(offset.y - 160f, 0.25f);
    }
}
