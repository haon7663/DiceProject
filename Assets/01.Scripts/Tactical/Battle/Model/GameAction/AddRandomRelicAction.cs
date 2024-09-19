using System.Linq;
using OneLine;
using UnityEngine;

[CreateAssetMenu(fileName = "AddRandomRelicAction", menuName = "GameActions/AddRandomRelicAction")]
public class AddRandomRelicAction : GameAction
{
    [OneLineWithHeader]
    public IntMinMax count;
    
    public override void Execute()
    {
        var relics = Resources.LoadAll<RelicSO>("Relics");
        for (var i = 0; i < count.GetValue(); i++)
        {
            var relic = relics.Where(relic =>
                DataManager.Inst.playerData.Relics.All(relicJson => relicJson.name != relic.name)).ToList().Random();
            DataManager.Inst.playerData.Relics.Add(relic.ToJson());
        }
    }
}