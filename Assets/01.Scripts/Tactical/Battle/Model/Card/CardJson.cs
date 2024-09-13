using System;

[Serializable]
public class CardJson
{
    public string name;
    public bool isUpgrade;

    public CardJson(string name, bool isUpgrade)
    {
        this.name = name;
        this.isUpgrade = isUpgrade;
    }
}