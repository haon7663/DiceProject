using System.Collections;
using System.Collections.Generic;
using OneLine;
using UnityEngine;

[CreateAssetMenu(fileName = "AddDiceAction", menuName = "GameActions/AddDiceAction")]
public class AddDiceAction : GameAction
{
    [OneLineWithHeader]
    public IntMinMax count;

    public DiceType diceType;
    
    public override void Execute()
    {
        for (var i = 0; i < count.GetValue(); i++)
        {
            DataManager.Inst.AddDices(diceType, 1);
        }
    }
}
