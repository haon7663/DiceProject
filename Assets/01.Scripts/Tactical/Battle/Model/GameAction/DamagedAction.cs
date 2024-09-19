using OneLine;
using UnityEngine;

[CreateAssetMenu(fileName = "DamagedAction", menuName = "GameActions/DamagedAction")]
public class DamagedAction : GameAction
{
    [OneLineWithHeader]
    public IntMinMax count;
    
    public override void Execute()
    {
        DataManager.Inst.playerData.Health -= count.GetValue();
    }
}