using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace Map
{
    public class Node
    {
        public readonly MapVector point;
        public readonly List<MapVector> incoming = new();
        public readonly List<MapVector> outgoing = new();
        
        [JsonConverter(typeof(StringEnumConverter))]
        public readonly NodeType nodeType;
        public readonly string blueprintName;
        public Vector2 position;

        public Node(NodeType nodeType, string blueprintName, MapVector point)
        {
            this.nodeType = nodeType;
            this.blueprintName = blueprintName;
            this.point = point;
        }

        public void AddIncoming(MapVector p)
        {
            if (incoming.Any(element => element.Equals(p)))
                return;

            incoming.Add(p);
        }

        public void AddOutgoing(MapVector p)
        {
            if (outgoing.Any(element => element.Equals(p)))
                return;

            outgoing.Add(p);
        }

        public void RemoveIncoming(MapVector p)
        {
            incoming.RemoveAll(element => element.Equals(p));
        }

        public void RemoveOutgoing(MapVector p)
        {
            outgoing.RemoveAll(element => element.Equals(p));
        }

        public bool HasNoConnections()
        {
            return incoming.Count == 0 && outgoing.Count == 0;
        }
    }
}