using OneLine;
using UnityEngine;

[CreateAssetMenu(fileName = "RecoveryAction", menuName = "GameActions/RecoveryAction")]
public class RecoveryAction : GameAction
{
    [OneLineWithHeader]
    public IntMinMax count;
    
    private int _saveCount;
    
    public override void Execute()
    {
        _saveCount = count.GetValue();
        DataManager.Inst.AddHealth(_saveCount);
    }

    public override string GetDialog()
    {
        return AddColor($"체력 {_saveCount} 회복", "green");
    }
}