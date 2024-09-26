using System.Collections;
using System.Collections.Generic;
using System.Text;
using OneLine;
using UnityEngine;

[CreateAssetMenu(fileName = "AddDiceAction", menuName = "GameActions/AddDiceAction")]
public class AddDiceAction : GameAction
{
    [OneLineWithHeader]
    public IntMinMax count;

    public DiceType diceType;

    private int _saveCount;
    
    public override void Execute()
    {
        _saveCount = count.GetValue();
        for (var i = 0; i < _saveCount; i++)
        {
            DataManager.Inst.AddDices(diceType, 1);
        }
    }
    
    public override string GetDialog()
    {
        return AddColor($"{diceType.GetDiceMaxValue().ToString()} 주사위 {_saveCount}개 획득");
    }
}
