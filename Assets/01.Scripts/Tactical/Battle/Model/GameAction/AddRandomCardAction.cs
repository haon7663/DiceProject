using System.Collections.Generic;
using System.Linq;
using System.Text;
using OneLine;
using UnityEngine;

[CreateAssetMenu(fileName = "AddRandomCardAction", menuName = "GameActions/AddRandomCardAction")]
public class AddRandomCardAction : GameAction
{
    [OneLineWithHeader]
    public IntMinMax count;

    private List<CardSO> _saveCards;
    
    public override void Execute()
    {
        _saveCards = new List<CardSO>();
        var cards = Resources.LoadAll<CardSO>("Cards/Player");
        for (var i = 0; i < count.GetValue(); i++)
        {
            var card = cards.Where(c =>
                DataManager.Inst.playerData.Cards.All(card => card.name != c.cardName)).ToList().Random();
            DataManager.Inst.playerData.Cards.Add(card.ToJson());
            
            _saveCards.Add(card);
        }
    }
    
    public override string GetDialog()
    {
        var stringBuilder = new StringBuilder();
        foreach (var card in _saveCards)
        {
            stringBuilder.Append($"\"{card.cardName}\" 카드 획득\n");
        }
        return AddColor(stringBuilder.ToString());
    }
}