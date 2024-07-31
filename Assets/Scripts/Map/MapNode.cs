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

    public void Init(Node node, Vector2 pos)
    {
        Node = node;

        var blueprint = Node.blueprint;
        image.sprite = blueprint.sprite;
        image.rectTransform.anchoredPosition = pos * 200 - new Vector2(0, 200);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
