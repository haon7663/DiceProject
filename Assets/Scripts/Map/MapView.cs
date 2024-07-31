using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI.Extensions;

namespace Map
{
    public class MapView : MonoBehaviour
    {
        public Map Map { get; private set; }
        
        [SerializeField] private GameObject nodePrefab;
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private Transform mapParent;

        [SerializeField] private int linePointCount;
        [SerializeField] private float offsetFromNodes;

        private List<MapNode> _mapNodes;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void ShowMap(Map map)
        {
            Map = map;
            
            CreateNodes(map.nodes);

            DrawLines();
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
            var mapNode = Instantiate(nodePrefab, mapParent).GetComponent<MapNode>();
            mapNode.Init(node, GetNodePosition(node));
            return mapNode;
        }
        
        private void DrawLines()
        {
            foreach (var mapNode in _mapNodes)
            {
                foreach (var connection in mapNode.Node.outgoing)
                    AddLineConnection(mapNode, GetNode(connection));
            }
        }

        private void AddLineConnection(MapNode from, MapNode to)
        {
            UILineRenderer lineRenderer = Instantiate(linePrefab, mapParent.transform).GetComponent<UILineRenderer>();
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
        }
        
        private MapNode GetNode(Node node)
        {
            return _mapNodes.FirstOrDefault(n => n.Node.Equals(node));
        }
        private Vector2 GetNodePosition(Node node)
        {
            return new Vector2(node.point.y, node.point.x);
        }

    }
}