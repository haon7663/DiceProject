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

    private float defaultScale = 1f;

    public void Init(Node node, NodeBlueprint blueprint, Vector2 pos)
    {
        Node = node;
        
        image.sprite = blueprint.sprite;
        image.rectTransform.anchoredPosition = pos * new Vector2(200, 175f);

        defaultScale = node.blueprintName == "Boss" ? 1.5f : 1;
    }

    public void SetColor(Color color)
    {
        image.color = color;
    }

    private Sequence _highlightSequence;  // 시퀀스를 저장할 변수

    public void SetHighlight()
    {
        // 시퀀스 생성 후 클래스 레벨 변수에 저장
        _highlightSequence = DOTween.Sequence();
    
        var originColor = image.color + new Color(0.25f, 0.25f, 0.25f);

        _highlightSequence.Append(image.rectTransform.DOSizeDelta(new Vector2(175, 175) * defaultScale, 1f).SetEase(Ease.InOutQuad));
        _highlightSequence.Join(image.DOColor(Color.white, 1f).SetEase(Ease.InOutQuad).From(originColor));
        _highlightSequence.Append(image.rectTransform.DOSizeDelta(new Vector2(150, 150) * defaultScale, 1f).SetEase(Ease.InOutQuad));
        _highlightSequence.Join(image.DOColor(originColor, 1f).SetEase(Ease.InOutQuad));

        _highlightSequence.SetLoops(-1, LoopType.Restart); // 무한 반복
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MapPlayerTracker.Inst.SelectNode(this);
    }

    public void OnSelect()
    {
        if (_highlightSequence != null && _highlightSequence.IsActive())
        {
            _highlightSequence.Kill();  // 시퀀스를 종료
        }

        image.rectTransform.DOSizeDelta(new Vector2(190, 190) * defaultScale, 0.5f).SetEase(Ease.OutQuint);
        image.DOColor(Color.white, 0.5f).SetEase(Ease.OutQuint);
    }
}
