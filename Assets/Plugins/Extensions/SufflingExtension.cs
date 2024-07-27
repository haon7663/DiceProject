using System;
using System.Collections.Generic;
using System.Linq;

public static class ShufflingExtension
{
    private static Random _random = new();

    public static void Shuffle<T>(this IList<T> list)
    {
        for (var i = list.Count - 1; i > 0; i--)
        {
            int indexToSwap = _random.Next(i + 1);
            (list[indexToSwap], list[i]) = (list[i], list[indexToSwap]);
        }
    }

    public static T Random<T>(this IList<T> list)
    {
        return list[_random.Next(list.Count)];
    }

    public static List<T> GetRandomElements<T>(this List<T> list, int elementsCount)
    {
        return list.OrderBy(arg => Guid.NewGuid()).Take(list.Count < elementsCount ? list.Count : elementsCount).ToList();
    }
}