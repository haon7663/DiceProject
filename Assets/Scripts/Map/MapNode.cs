using System.Collections;
using System.Collections.Generic;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        print("ClickNode");
        MapPlayerTracker.Inst.SelectNode(this);
    }

    public void SetColor(Color color)
    {
        image.color = color;
    }
}
