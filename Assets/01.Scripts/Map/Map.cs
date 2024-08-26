using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Map
{
    public class Map
    {
        public List<Node> nodes;
        public List<MapVector> path;
        public string bossNodeName;
        public string configName;

        public Map(string configName, string bossNodeName, List<Node> nodes, List<MapVector> path)
        {
            this.configName = configName;
            this.bossNodeName = bossNodeName;
            this.nodes = nodes;
            this.path = path;
        }

        public Node GetBossNode()
        {
            return nodes.FirstOrDefault(n => n.nodeType == NodeType.Boss);
        }

        public Node GetNode(MapVector p)
        {
            return nodes.FirstOrDefault(n => n.point.Equals(p));
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}