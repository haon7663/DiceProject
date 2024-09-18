using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AddRandomCardAction", menuName = "GameActions/AddRandomCardAction")]
public class AddRandomCardAction : GameAction
{
    public int count;
    
    public override void Execute()
    {
        var cards = Resources.LoadAll<CardSO>("Cards");
        for (var i = 0; i < count; i++)
        {
            var card = cards.Where(c =>
                DataManager.Inst.playerData.Cards.All(card => card.name != c.cardName)).ToList().Random();
            DataManager.Inst.playerData.Cards.Add(card.ToJson());
        }
    }
}