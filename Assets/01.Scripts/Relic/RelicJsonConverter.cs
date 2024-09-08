using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RelicJsonConverter
{
    public static RelicJson ToJson(this RelicSO relicSO)
    {
        return new RelicJson(relicSO.name);
    }

    public static List<RelicJson> ToJson(this List<RelicSO> relics)
    {
        foreach (var c in relics.Select(relic => relic.name))
        {
            Debug.Log(c);
        }
        return relics.Select(relic => relic.ToJson()).ToList();
    }

    public static RelicSO ToRelic(this RelicJson relicJson)
    {
        var relics = Resources.LoadAll<RelicSO>("Relics");
        var relic = relics.First(relic => relic.name == relicJson.name);
        
        if (relic)
            return relic;
        
        Debug.LogWarning("Relic was null in RelicJsonConverter.ToRelic()");
        return null;
    }
    
    public static List<RelicSO> ToRelic(this List<RelicJson> relicJsons)
    {
        return relicJsons.Select(relic => relic.ToRelic()).ToList();
    }
}