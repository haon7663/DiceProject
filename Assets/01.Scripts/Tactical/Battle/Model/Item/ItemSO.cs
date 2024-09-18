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
    
    [Header("효과")]
    public List<BehaviourInfo> behaviourInfos;
    public List<GameAction> actions;
    
    public void ExecuteActions()
    {
        foreach (var action in actions)
        {
            action.Execute();
        }
    }
}