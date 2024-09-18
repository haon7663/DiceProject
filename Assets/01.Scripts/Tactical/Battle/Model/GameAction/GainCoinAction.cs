using OneLine;
using UnityEngine;

[CreateAssetMenu(fileName = "GainCoinAction", menuName = "GameActions/GainCoinAction")]
public class GainCoinAction : GameAction
{
    [OneLineWithHeader]
    public IntMinMax count;
    
    public override void Execute()
    {
        DataManager.Inst.playerData.Gold += count.GetValue();
    }
}