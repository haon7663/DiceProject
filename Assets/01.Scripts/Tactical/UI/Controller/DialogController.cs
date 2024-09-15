using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private Dialog dialogPrefab;
    
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform contentRect;
    
    [ContextMenu("GenerateDialog")]
    public void GenerateDialog()
    {
        var dialog = Instantiate(dialogPrefab, contentRect.transform);
        SetParentPosition(dialog);
    }
    
    private void SetParentPosition(Dialog dialog)
    {
        DOTween.Kill(contentRect);
        Canvas.ForceUpdateCanvases();

        Vector2 contentPos = scrollRect.transform.InverseTransformPoint(contentRect.position);
        Vector2 dialogPos = scrollRect.transform.InverseTransformPoint(dialog.transform.position);
        Vector2 offset = contentPos - dialogPos;

        float contentMinY = -contentRect.sizeDelta.y;
        float contentMaxY = 0f;

        float targetPosY = Mathf.Clamp(offset.y + 540f, contentMinY, contentMaxY);

        contentRect.DOAnchorPosY(targetPosY, 0.25f);
    }

}
