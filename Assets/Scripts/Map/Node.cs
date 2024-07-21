using UnityEngine;

namespace Map
{
    public enum NodeType
    {
        MinorEnemy,
        EliteEnemy,
        RestSite,
        Treasure,
        Store,
        Boss,
        Mystery
    }
    
    public class Node
    {
        public readonly NodeType NodeType;
        public readonly Vector2Int Point;
        public readonly Vector2 Position;

        public Node(NodeType nodeType, Vector2Int point)
        {
            NodeType = nodeType;
            Point = point;
        }
    }
}