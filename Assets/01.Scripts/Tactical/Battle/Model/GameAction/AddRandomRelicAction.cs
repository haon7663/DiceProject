using System.Collections.Generic;
using System.Linq;
using System.Text;
using OneLine;
using UnityEngine;

[CreateAssetMenu(fileName = "AddRandomRelicAction", menuName = "GameActions/AddRandomRelicAction")]
public class AddRandomRelicAction : GameAction
{
    [OneLineWithHeader]
    public IntMinMax count;

    private List<RelicSO> _relicSos;
    
    public override void Execute()
    {
        _relicSos = new List<RelicSO>();
        var relics = Resources.LoadAll<RelicSO>("Relics");
        for (var i = 0; i < count.GetValue(); i++)
        {
            relics = relics.Where(relic => DataManager.Inst.playerData.Relics.All(relicJson => relicJson.name != relic.name)).ToArray();
            if (relics.Length < 1) continue;
            var relic = relics.Random();
            DataManager.Inst.playerData.Relics.Add(relic.ToJson());
            
            _relicSos.Add(relic);
        }
    }
    
    public override string GetDialog()
    {
        var stringBuilder = new StringBuilder();
        foreach (var relic in _relicSos)
        {
            stringBuilder.Append($"\"{relic.name}\" 획득\n");
        }
        return AddColor(stringBuilder.ToString());
    }
}