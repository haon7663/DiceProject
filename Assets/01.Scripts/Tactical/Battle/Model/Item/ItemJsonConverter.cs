using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ItemJsonConverter
{
    public static string ToJson(this ItemSO itemSO)
    {
        return itemSO.itemName;
    }

    public static List<string> ToJson(this List<ItemSO> itemSOs)
    {
        return itemSOs.Select(itemSO => itemSO.ToJson()).ToList();
    }

    public static ItemSO ToItem(this string name)
    {
        var items = Resources.LoadAll<ItemSO>("Items");
        var item = items.FirstOrDefault(itemSO => itemSO.itemName == name);
        if (item)
            return item;
        Debug.LogWarning("Item was null in ItemJsonConverter.ToItem()");
        return null;
    }
    
    public static List<ItemSO> ToItem(this List<string> names)
    {
        return names.Select(name => name.ToItem()).ToList();
    }
}