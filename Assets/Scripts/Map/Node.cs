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
        public readonly List<Node> incoming = new();
        public readonly List<Node> outgoing = new();
        
        [JsonConverter(typeof(StringEnumConverter))]
        public readonly NodeType nodeType;
        public readonly NodeBlueprint blueprint;

        public Node(NodeType nodeType, NodeBlueprint blueprint, Vector2Int point)
        {
            this.nodeType = nodeType;
            this.blueprint = blueprint;
            this.point = point;
        }

        public void AddIncoming(Node node)
        {
            if (incoming.Any(element => element.Equals(node)))
                return;

            incoming.Add(node);
        }

        public void AddOutgoing(Node node)
        {
            if (outgoing.Any(element => element.Equals(node)))
                return;

            outgoing.Add(node);
        }

        public void RemoveIncoming(Node node)
        {
            incoming.RemoveAll(element => element.Equals(node));
        }

        public void RemoveOutgoing(Node node)
        {
            outgoing.RemoveAll(element => element.Equals(node));
        }

        public bool HasNoConnections()
        {
            return incoming.Count == 0 && outgoing.Count == 0;
        }
    }
}