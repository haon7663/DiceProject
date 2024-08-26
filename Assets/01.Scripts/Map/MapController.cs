using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Map
{
    public class MapController : MonoBehaviour
    {
        public MapConfig config;
        public MapView view;
        public Map CurrentMap { get; private set; }

        private static string _mapFileString;

        private void Start()
        {
            _mapFileString = Path.Combine(Application.persistentDataPath, "map.json");
            
            if (File.Exists(_mapFileString))
            {
                Load();
            }
            else
            {
                GenerateNewMap();
            }
        }

        public void GenerateNewMap()
        {
            var map = MapGenerator.GetMap(config);
            CurrentMap = map;
            view.ShowMap(map);
        }

        private void Load()
        {
            var mapJson = File.ReadAllText(_mapFileString);
            CurrentMap = JsonConvert.DeserializeObject<Map>(mapJson);
            view.ShowMap(CurrentMap);
        }
        
        public void Save()
        {
            if (CurrentMap == null) return;

            string json = JsonConvert.SerializeObject(CurrentMap, Formatting.Indented,
                new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            
            File.WriteAllText(_mapFileString, json);
        }

        private void OnApplicationQuit()
        {
            Save();
        }
    }
}