using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public static class MapGenerator
    {
        private static List<List<Node>> _nodes;

        public static void Get()
        {
            for (var i = 0; i < 10; i++)
            {
                PlaceLayer(i);
            }
        }
        
        private static void PlaceLayer(int layerIndex)
        {
            var nodesOnThisLayer = new List<Node>();

            // offset of this layer to make all the nodes centered:
            var offsetX = 2 * 4 / 2f;

            for (var i = 0; i < 4; i++)
            {
                var nodeType = NodeType.MinorEnemy;
                Node node = new Node(nodeType, new Vector2Int(layerIndex, i));
                nodesOnThisLayer.Add(node);
            }

            _nodes.Add(nodesOnThisLayer);
        }
        
        /*private static void SetUpConnections(List<List<Vector2Int>> paths)
        {
            foreach (List<Vector2Int> path in paths)
            {
                for (int i = 0; i < path.Count - 1; ++i)
                {
                    Node node = GetNode(path[i]);
                    Node nextNode = GetNode(path[i + 1]);
                    node.AddOutgoing(nextNode.point);
                    nextNode.AddIncoming(node.point);
                }
            }
        }*/
    }
}