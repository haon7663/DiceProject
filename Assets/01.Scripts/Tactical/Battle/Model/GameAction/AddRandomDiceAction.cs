using OneLine;
using UnityEngine;

[CreateAssetMenu(fileName = "AddRandomDiceAction", menuName = "GameActions/AddRandomDiceAction")]
public class AddRandomDiceAction : GameAction
{
    [OneLineWithHeader]
    public IntMinMax count;
    
    public override void Execute()
    {
        for (var i = 0; i < count.GetValue(); i++)
        {
            var diceType = (DiceType)Random.Range(1, 4);
            DataManager.Inst.playerData.Dices[diceType]++;
        }
    }
}
