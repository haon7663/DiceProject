using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace Map
{
    public class Node
    {
        public readonly Vector2Int point;
        public readonly List<Vector2Int> incoming = new();
        public readonly List<Vector2Int> outgoing = new();
        
        [JsonConverter(typeof(StringEnumConverter))]
        public readonly NodeType nodeType;
        public readonly NodeBlueprint blueprint;

        public Node(NodeType nodeType, NodeBlueprint blueprint, Vector2Int point)
        {
            this.nodeType = nodeType;
            this.blueprint = blueprint;
            this.point = point;
        }

        public void AddIncoming(Vector2Int p)
        {
            if (incoming.Any(element => element.Equals(p)))
                return;

            incoming.Add(p);
        }

        public void AddOutgoing(Vector2Int p)
        {
            if (outgoing.Any(element => element.Equals(p)))
                return;

            outgoing.Add(p);
        }

        public void RemoveIncoming(Vector2Int p)
        {
            incoming.RemoveAll(element => element.Equals(p));
        }

        public void RemoveOutgoing(Vector2Int p)
        {
            outgoing.RemoveAll(element => element.Equals(p));
        }

        public bool HasNoConnections()
        {
            return incoming.Count == 0 && outgoing.Count == 0;
        }
    }
}