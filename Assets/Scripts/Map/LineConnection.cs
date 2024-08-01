using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI.Extensions;

namespace Map
{
    [System.Serializable]
    public class LineConnection
    {
        public UILineRenderer uiLineRenderer; 
        public MapNode from;
        public MapNode to;

        public LineConnection(UILineRenderer uiLineRenderer, MapNode from, MapNode to)
        {
            this.uiLineRenderer = uiLineRenderer;
            this.from = from;
            this.to = to;
        }

        public void SetColor(Color color)
        {
            uiLineRenderer.color = color;
        }
    }
}