using System;
using Map;
using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private MapView mapView;
        [SerializeField] private MapConfig mapConfig;

        private Map _map;

        private void Start()
        {
            _map = MapGenerator.GetMap(mapConfig);
            mapView.ShowMap(_map);
        }
        
        /*private void OnDrawGizmos()
        {
            foreach (var node in _map.nodes)
            {
                foreach (var connection in node.outgoing)
                {
                    Gizmos.DrawLine((Vector2)node.point, (Vector2)connection.point);
                    Gizmos.DrawWireSphere((Vector2)node.point, 0.25f);
                }
            }
        }*/
    }
}