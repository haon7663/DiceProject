using OneLine;
using UnityEngine;

namespace Map
{
    [System.Serializable]
    public class MapLayer
    {
        public NodeType nodeType;
        [Range(0f, 1f)] public float randomizeNodes;
    }
}