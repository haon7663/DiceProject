using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace Map
{
    public class MapView : MonoBehaviour
    {
        public Map Map { get; private set; }
        
        [SerializeField] private GameObject nodePrefab;
        [SerializeField] private GameObject linePrefab;

        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform contentRect;

        [SerializeField] private int linePointCount;
        [SerializeField] private float offsetFromNodes;
        
        [SerializeField] private float padding;

        [SerializeField] private Color visitedColor;
        [SerializeField] private Color lockedColor;

        private List<MapNode> _mapNodes;
        private List<LineConnection> _lineConnections;

        public void ShowMap(Map map)
        {
            Map = map;

            ClearMap();
            
            CreateNodes(map.nodes);

            SetParentPosition();

            DrawLines();

            SetNodeColor();
            SetLineColor();
        }

        private void ClearMap()
        {
            for (var i = 0; i < contentRect.childCount; i++)
                Destroy(contentRect.GetChild(i).gameObject);
            
            _mapNodes = new List<MapNode>();
            _lineConnections = new List<LineConnection>();
        }
        
        private void CreateNodes(List<Node> nodes)
        {
            _mapNodes = new List<MapNode>();
            foreach (var node in nodes)
            {
                _mapNodes.Add(CreateMapNode(node));
            }
        }
        
        private MapNode CreateMapNode(Node node)
        {
            var mapNode = Instantiate(nodePrefab, contentRect.transform).GetComponent<MapNode>();
            mapNode.Init(node, GetBlueprint(node.nodeType), GetNodePosition(node));
            return mapNode;
        }
        
        private void DrawLines()
        {
            _lineConnections = new List<LineConnection>();
            foreach (var mapNode in _mapNodes)
            {
                foreach (var connection in mapNode.Node.outgoing)
                    AddLineConnection(mapNode, GetNode(connection));
            }
        }

        private void AddLineConnection(MapNode from, MapNode to)
        {
            UILineRenderer lineRenderer = Instantiate(linePrefab, contentRect.transform).GetComponent<UILineRenderer>();
            lineRenderer.transform.SetAsFirstSibling();
            
            RectTransform fromRT = from.transform as RectTransform;
            RectTransform toRT = to.transform as RectTransform;
            Vector2 fromPoint = fromRT.anchoredPosition +
                                (toRT.anchoredPosition - fromRT.anchoredPosition).normalized * offsetFromNodes;

            Vector2 toPoint = toRT.anchoredPosition +
                              (fromRT.anchoredPosition - toRT.anchoredPosition).normalized * offsetFromNodes;
            
            lineRenderer.transform.position = from.transform.position +
                                              (Vector3) (toRT.anchoredPosition - fromRT.anchoredPosition).normalized *
                                              offsetFromNodes;
            
            List<Vector2> list = new List<Vector2>();
            for (int i = 0; i < linePointCount; i++)
            {
                list.Add(Vector3.Lerp(Vector3.zero, toPoint - fromPoint +
                                                    2 * (fromRT.anchoredPosition - toRT.anchoredPosition).normalized *
                                                    offsetFromNodes, (float) i / (linePointCount - 1)));
            }

            lineRenderer.Points = list.ToArray();
            
            _lineConnections.Add(new LineConnection(lineRenderer, from, to));
        }

        private void SetParentPosition()
        {
            if (Map.path.Count == 0) return;
                
            Canvas.ForceUpdateCanvases();

            Vector2 contentPos = scrollRect.transform.InverseTransformPoint(contentRect.position);
            Vector2 targetPos = scrollRect.transform.InverseTransformPoint(GetNode(Map.path[^1]).transform.position);
            Vector2 offset = contentPos - targetPos;

            float contentMinX = -contentRect.sizeDelta.x;
            float contentMaxX = 0f;

            float targetPosX = Mathf.Clamp(offset.x + 540f, contentMinX, contentMaxX);
            
            contentRect.anchoredPosition = new Vector2(targetPosX, 0);
        }

        public void SetNodeColor()
        {
            foreach (var mapNode in _mapNodes)
            {                
                mapNode.SetColor(Map.path.Any(v => v.Equals(mapNode.Node.point)) ? visitedColor : lockedColor);
            }
        }
        
        public void SetLineColor()
        {
            foreach (var lineConnection in _lineConnections)
            {                
                lineConnection.SetColor(Map.path.Any(v => v.Equals(lineConnection.from.Node.point))
                    && Map.path.Any(v => v.Equals(lineConnection.to.Node.point)) ? visitedColor : lockedColor);
            }
            
            /*var currentPoint = Map.path[^1];
            var currentNode = Map.GetNode(currentPoint);
            foreach (var point in currentNode.outgoing)
            {
                LineConnection lineConnection = _lineConnections.FirstOrDefault(conn => conn.from.Node == currentNode &&
                    conn.to.Node.point.Equals(point));
                lineConnection?.SetColor(lockedColor);
            }*/
        }
        
        private MapNode GetNode(MapVector p)
        {
            return _mapNodes.FirstOrDefault(n => n.Node.point.Equals(p));
        }
        private Vector2 GetNodePosition(Node node)
        {
            float length = padding + Map.DistanceBetweenFirstAndLastLayers();
            return new Vector2((padding - length) / 2 + node.point.Y, node.point.X);
        }
        private MapConfig GetConfig(string configName)
        {
            return Resources.LoadAll<MapConfig>("MapConfigs").FirstOrDefault(c => c.name == configName);
        }

        private NodeBlueprint GetBlueprint(NodeType type)
        {
            return Resources.LoadAll<NodeBlueprint>("NodeBlueprints").FirstOrDefault(n => n.nodeType == type);
        }
        private NodeBlueprint GetBlueprint(string blueprintName)
        {
            return Resources.LoadAll<NodeBlueprint>("NodeBlueprints").FirstOrDefault(n => n.name == blueprintName);
        }
    }
}