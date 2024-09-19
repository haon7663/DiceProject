using System.Linq;
using OneLine;
using UnityEngine;

[CreateAssetMenu(fileName = "AddRandomCardAction", menuName = "GameActions/AddRandomCardAction")]
public class AddRandomCardAction : GameAction
{
    [OneLineWithHeader]
    public IntMinMax count;
    
    public override void Execute()
    {
        var cards = Resources.LoadAll<CardSO>("Cards");
        for (var i = 0; i < count.GetValue(); i++)
        {
            var card = cards.Where(c =>
                DataManager.Inst.playerData.Cards.All(card => card.name != c.cardName)).ToList().Random();
            DataManager.Inst.playerData.Cards.Add(card.ToJson());
        }
    }
}