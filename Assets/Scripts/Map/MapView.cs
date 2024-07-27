using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] private GameObject nodePrefab;
        [SerializeField] private Transform mapParent;

        public void ShowMap(Map map)
        {
            CreateNodes(map.nodes);
        }
        
        private void CreateNodes(List<Node> nodes)
        {
            foreach (var node in nodes)
            {
                CreateMapNode(node);
            }
        }
        
        private MapNode CreateMapNode(Node node)
        {
            var mapNode = Instantiate(nodePrefab, mapParent).GetComponent<MapNode>();
            mapNode.Init(node, GetNodePosition(node));
            return mapNode;
        }
        
        private Vector2 GetNodePosition(Node node)
        {
            return new Vector2(node.point.y, node.point.x);
        }
    }
}