using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Relic : MonoBehaviour
{
    public List<RelicSO> relics;

    public List<BehaviourInfo> GetInfosAndExecute(RelicActiveType relicActiveType, StatusEffectSO statusEffectSO = null)
    {
        var behaviourInfos = new List<BehaviourInfo>();
        var selectedRelics = relics.Where(r => r.relicActiveType == relicActiveType);
        foreach (var relic in selectedRelics)
        {
            relic.ExecuteActions();
            behaviourInfos.AddRange(relic.behaviourInfos);
        }
        return behaviourInfos;
    }
}