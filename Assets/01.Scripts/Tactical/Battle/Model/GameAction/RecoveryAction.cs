using OneLine;
using UnityEngine;

[CreateAssetMenu(fileName = "RecoveryAction", menuName = "GameActions/RecoveryAction")]
public class RecoveryAction : GameAction
{
    [OneLineWithHeader]
    public IntMinMax count;
    
    public override void Execute()
    {
        DataManager.Inst.playerData.Health += count.GetValue();
    }
}