using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Map;
using UnityEngine.EventSystems;

public class MapNode : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image image;
    public Node Node { get; private set; }

    public void Init(Node node, NodeBlueprint blueprint, Vector2 pos)
    {
        Node = node;
        
        image.sprite = blueprint.sprite;
        image.rectTransform.anchoredPosition = pos * 200 - new Vector2(0, 200);
    }

    public void SetColor(Color color)
    {
        image.color = color;
    }

    public void SetHighlight()
    {
        var sequence = DOTween.Sequence();
        
        var originColor = image.color + new Color(0.25f, 0.25f, 0.25f);

        sequence.Append(image.rectTransform.DOSizeDelta(new Vector2(175, 175), 1f).SetEase(Ease.InOutQuad));
        sequence.Join(image.DOColor(Color.white, 1f).SetEase(Ease.InOutQuad).From(originColor));
        sequence.Append(image.rectTransform.DOSizeDelta(new Vector2(150, 150), 1f).SetEase(Ease.InOutQuad));
        sequence.Join(image.DOColor(originColor, 1f).SetEase(Ease.InOutQuad));

        sequence.SetLoops(-1, LoopType.Restart);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        MapPlayerTracker.Inst.SelectNode(this);
    }
}
