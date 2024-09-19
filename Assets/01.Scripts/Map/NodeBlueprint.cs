using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace Map
{
    public enum NodeType
    {
        MinorEnemy,
        EliteEnemy,
        RestSite,
        Store,
        Boss,
        Mystery,
        None
    }
}

namespace Map
{
    [CreateAssetMenu(menuName = "Scriptable Object/Map/NodeBluePrint", fileName = "NodeBluePrint")]
    public class NodeBlueprint : ScriptableObject
    {
        public Sprite sprite;
        public NodeType nodeType;
    }
}