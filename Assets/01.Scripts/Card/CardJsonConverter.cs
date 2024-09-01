using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CardJsonConverter
{
    public static CardJson ToJson(this CardSO cardSO)
    {
        return new CardJson(cardSO.cardName, false);
    }

    public static List<CardJson> ToJson(this List<CardSO> cardSOs)
    {
        foreach (var c in cardSOs.Select(cardSO => cardSO.cardName))
        {
            Debug.Log(c);
        }
        return cardSOs.Select(cardSO => cardSO.ToJson()).ToList();
    }

    public static CardSO ToCard(this CardJson cardJson)
    {
        var cards = Resources.LoadAll<CardSO>("Cards");
        var card = cards.First(card => card.cardName == cardJson.cardName);
        if (card)
            return card;
        Debug.LogWarning("Card was null in CardJsonConverter.ToCard()");
        return null;
    }
    
    public static List<CardSO> ToCard(this List<CardJson> cardJsons)
    {
        return cardJsons.Select(cardSO => cardSO.ToCard()).ToList();
    }
}
