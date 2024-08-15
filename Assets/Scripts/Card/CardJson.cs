using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardJson
{
    public string cardName;
    public bool isUpgrade;

    public CardJson(string cardName, bool isUpgrade)
    {
        this.cardName = cardName;
        this.isUpgrade = isUpgrade;
    }
}