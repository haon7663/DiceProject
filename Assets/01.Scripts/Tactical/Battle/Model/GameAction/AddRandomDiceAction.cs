using System.Collections.Generic;
using System.Text;
using OneLine;
using UnityEngine;

[CreateAssetMenu(fileName = "AddRandomDiceAction", menuName = "GameActions/AddRandomDiceAction")]
public class AddRandomDiceAction : GameAction
{
    [OneLineWithHeader]
    public IntMinMax count;
    
    private List<DiceType> _saveDices;
    
    public override void Execute()
    {
        _saveDices = new List<DiceType>();
        for (var i = 0; i < count.GetValue(); i++)
        {
            var diceType = (DiceType)Random.Range(1, 4);
            DataManager.Inst.AddDices(diceType, 1);
            _saveDices.Add(diceType);
        }
    }

    public override string GetDialog()
    {
        var stringBuilder = new StringBuilder();
        foreach (var diceType in _saveDices)
        {
            stringBuilder.Append($"{diceType.GetDiceMaxValue().ToString()} 주사위 획득\n");
        }
        return AddColor(stringBuilder.ToString());
    }
}
