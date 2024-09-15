using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/Item")]
public class ItemSO : ScriptableObject
{
    [Header("정보")]
    public Sprite sprite;
    public string itemName;
    [TextArea] public string description;
    
    [Header("능력치")]
    public List<BehaviourInfo> behaviourInfos;
}