using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
    public static class MapGenerator
    {
        private static MapConfig _config;

        private static readonly List<NodeType> RandomNodes = new List<NodeType>
        {NodeType.Mystery, NodeType.Store, NodeType.Treasure, NodeType.MinorEnemy, NodeType.RestSite};

        private static List<float> _layerDistances;
        
        private static readonly List<List<Node>> nodes = new List<List<Node>>();

        public static Map GetMap(MapConfig config)
        {
            if (config == null)
            {
                Debug.LogWarning("Config was null in MapGenerator.Generate()");
                return null;
            }

            _config = config;
            nodes.Clear();

            for (int i = 0; i < config.layers.Count; i++)
                PlaceLayer(i);

            List<List<MapVector>> paths = GeneratePaths();

            //RandomizeNodePositions();

            SetUpConnections(paths);

            RemoveCrossConnections();
            
            List<Node> nodesList = nodes.SelectMany(n => n).Where(n => n.incoming.Count > 0 || n.outgoing.Count > 0).ToList();
            
            string bossNodeName = _config.nodeBlueprints.Where(b => b.nodeType == NodeType.Boss).ToList().Random().name;
            return new Map(config.name, bossNodeName, nodesList, new List<MapVector>());
        }
        
        private static void PlaceLayer(int layerIndex)
        {
            MapLayer layer = _config.layers[layerIndex];
            List<Node> nodesOnThisLayer = new List<Node>();
            
            float offset = 2 * _config.GridWidth / 2f;
            Debug.LogWarning(_config.GridWidth);

            for (int i = 0; i < _config.GridWidth; i++)
            {
                NodeType nodeType = Random.Range(0f, 1f) < layer.randomizeNodes ? RandomNodes.Random() : layer.nodeType;
                var blueprint = _config.nodeBlueprints.Where(b => b.nodeType == nodeType).ToList().Random();
                Node node = new Node(nodeType, blueprint.name, new MapVector(i, layerIndex))
                {
                    position = new Vector2(-offset + i * 2, layerIndex)
                };
                Debug.Log(new Vector2(-offset + i * 2, layerIndex));
                nodesOnThisLayer.Add(node);
            }

            nodes.Add(nodesOnThisLayer);
        }

        /*private static void RandomizeNodePositions()
        {
            for (int index = 0; index < nodes.Count; index++)
            {
                List<Node> list = nodes[index];
                MapLayer layer = _config.layers[index];
                float distToNextLayer = index + 1 >= _layerDistances.Count
                    ? 0f
                    : _layerDistances[index + 1];
                float distToPreviousLayer = _layerDistances[index];

                foreach (Node node in list)
                {
                    float xRnd = Random.Range(-0.5f, 0.5f);
                    float yRnd = Random.Range(-0.5f, 0.5f);

                    float x = xRnd * layer.nodesApartDistance;
                    float y = yRnd < 0 ? distToPreviousLayer * yRnd: distToNextLayer * yRnd;

                    node.position += new Vector2(x, y) * layer.randomizePosition;
                }
            }
        }*/

        private static void SetUpConnections(List<List<MapVector>> paths)
        {
            foreach (List<MapVector> path in paths)
            {
                for (int i = 0; i < path.Count - 1; ++i)
                {
                    Node node = GetNode(path[i]);
                    Node nextNode = GetNode(path[i + 1]);
                    node.AddOutgoing(nextNode.point);
                    nextNode.AddIncoming(node.point);
                }
            }
        }

        private static void RemoveCrossConnections()
        {
            for (int i = 0; i < _config.GridWidth - 1; ++i)
                for (int j = 0; j < _config.layers.Count - 1; ++j)
                {
                    Node node = GetNode(new MapVector(i, j));
                    if (node == null || node.HasNoConnections()) continue;
                    Node right = GetNode(new MapVector(i + 1, j));
                    if (right == null || right.HasNoConnections()) continue;
                    Node top = GetNode(new MapVector(i, j + 1));
                    if (top == null || top.HasNoConnections()) continue;
                    Node topRight = GetNode(new MapVector(i + 1, j + 1));
                    if (topRight == null || topRight.HasNoConnections()) continue;
                    
                    if (!node.outgoing.Any(element => element.Equals(topRight.point))) continue;
                    if (!right.outgoing.Any(element => element.Equals(top.point))) continue;
                    
                    node.AddOutgoing(top.point);
                    top.AddIncoming(node.point);

                    right.AddOutgoing(topRight.point);
                    topRight.AddIncoming(right.point);

                    float rnd = Random.Range(0f, 1f);
                    if (rnd < 0.2f)
                    {
                        node.RemoveOutgoing(topRight.point);
                        topRight.RemoveIncoming(node.point);
                        right.RemoveOutgoing(top.point);
                        top.RemoveIncoming(right.point);
                    }
                    else if (rnd < 0.6f)
                    {
                        node.RemoveOutgoing(topRight.point);
                        topRight.RemoveIncoming(node.point);
                    }
                    else
                    {
                        right.RemoveOutgoing(top.point);
                        top.RemoveIncoming(right.point);
                    }
                }
        }

        private static Node GetNode(MapVector p)
        {
            if (p.Y >= nodes.Count) return null;
            if (p.X >= nodes[p.Y].Count) return null;

            return nodes[p.Y][p.X];
        }

        private static MapVector GetFinalNode()
        {
            int y = _config.layers.Count - 1;
            if (_config.GridWidth % 2 == 1)
                return new MapVector(_config.GridWidth / 2, y);

            return Random.Range(0, 2) == 0
                ? new MapVector(_config.GridWidth / 2, y)
                : new MapVector(_config.GridWidth / 2 - 1, y);
        }

        private static List<List<MapVector>> GeneratePaths()
        {
            MapVector finalNode = GetFinalNode();
            var paths = new List<List<MapVector>>();
            int numOfStartingNodes = _config.numOfStartingNodes.GetValue();
            int numOfPreBossNodes = _config.numOfPreBossNodes.GetValue();

            List<int> candidateXs = new List<int>();
            for (int i = 0; i < _config.GridWidth; i++)
                candidateXs.Add(i);

            candidateXs.Shuffle();
            IEnumerable<int> startingXs = candidateXs.Take(numOfStartingNodes);
            List<MapVector> startingPoints = (from x in startingXs select new MapVector(x, 0)).ToList();

            candidateXs.Shuffle();
            IEnumerable<int> preBossXs = candidateXs.Take(numOfPreBossNodes);
            List<MapVector> preBossPoints = (from x in preBossXs select new MapVector(x, finalNode.Y - 1)).ToList();

            int numOfPaths = Mathf.Max(numOfStartingNodes, numOfPreBossNodes) + Mathf.Max(0, _config.extraPaths);
            for (int i = 0; i < numOfPaths; ++i)
            {
                MapVector startNode = startingPoints[i % numOfStartingNodes];
                MapVector endNode = preBossPoints[i % numOfPreBossNodes];
                List<MapVector> path = Path(startNode, endNode);
                path.Add(finalNode);
                paths.Add(path);
            }

            return paths;
        }
        
        private static List<MapVector> Path(MapVector fromPoint, MapVector toPoint)
        {
            int toRow = toPoint.Y;
            int toCol = toPoint.X;

            int lastNodeCol = fromPoint.X;

            List<MapVector> path = new List<MapVector> { fromPoint };
            List<int> candidateCols = new List<int>();
            for (int row = 1; row < toRow; ++row)
            {
                candidateCols.Clear();

                int verticalDistance = toRow - row;
                int horizontalDistance;

                int forwardCol = lastNodeCol;
                horizontalDistance = Mathf.Abs(toCol - forwardCol);
                if (horizontalDistance <= verticalDistance)
                    candidateCols.Add(lastNodeCol);

                int leftCol = lastNodeCol - 1;
                horizontalDistance = Mathf.Abs(toCol - leftCol);
                if (leftCol >= 0 && horizontalDistance <= verticalDistance)
                    candidateCols.Add(leftCol);

                int rightCol = lastNodeCol + 1;
                horizontalDistance = Mathf.Abs(toCol - rightCol);
                if (rightCol < _config.GridWidth && horizontalDistance <= verticalDistance)
                    candidateCols.Add(rightCol);
                
                int candidateCol = candidateCols[Random.Range(0, candidateCols.Count)];
                var nextPoint = new MapVector(candidateCol, row);

                path.Add(nextPoint);

                lastNodeCol = candidateCol;
            }

            path.Add(toPoint);

            return path;
        }
    }
}