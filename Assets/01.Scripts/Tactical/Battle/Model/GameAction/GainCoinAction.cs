using OneLine;
using UnityEngine;

[CreateAssetMenu(fileName = "GainCoinAction", menuName = "GameActions/GainCoinAction")]
public class GainCoinAction : GameAction
{
    [OneLineWithHeader]
    public IntMinMax count;
    
    private int _saveCount;
    
    public override void Execute()
    {
        _saveCount = count.GetValue();
        DataManager.Inst.playerData.Gold += _saveCount;
    }

    public override string GetDialog()
    {
        return AddColor($"{_saveCount} 골드 획득");
    }
}