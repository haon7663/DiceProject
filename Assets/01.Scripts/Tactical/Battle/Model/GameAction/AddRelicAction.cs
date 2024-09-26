using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "AddRelicAction", menuName = "GameActions/AddRelicAction")]
public class AddRelicAction : GameAction
{
    public RelicSO relicSO;
    
    public override void Execute()
    {
        DataManager.Inst.playerData.Relics.Add(relicSO.ToJson());
    }
        
    public override string GetDialog()
    {
        return AddColor($"\"{relicSO.name}\" 획득");
    }
}