using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreState : BattleState
{
    public override void Enter()
    {
        base.Enter();

        var selectedRelics = new List<RelicSO>();
        var relics = Resources.LoadAll<RelicSO>("Relics");
        for (var i = 0; i < 4; i++)
        {
            var gainRelics = new List<RelicSO>();
            gainRelics.AddRange(DataManager.Inst.playerData.Relics.ToRelic());
            gainRelics.AddRange(selectedRelics);
            
            var targetRelics = relics.Where(relic => gainRelics.All(relicJson => relicJson.name != relic.name)).ToArray();
            if (targetRelics.Length < 1) continue;
            var relic = targetRelics.Random();
            if (!relic) continue;
            
            selectedRelics.Add(relic);
        }
        
        var selectedItems = new List<ItemSO>();
        var items = Resources.LoadAll<ItemSO>("Items");
        for (var i = 0; i < 4; i++)
        {
            var item = items.Random();
            if (!item) continue;
            selectedItems.Add(item);
        }
        
        owner.storePanelController.GeneratePanels(selectedItems, selectedRelics);
    }
    
    protected override void AddListeners()
    {
        BattleInputController.ButtonEvent += OnButton;
    }
    
    protected override void RemoveListeners()
    {
        BattleInputController.ButtonEvent -= OnButton;
    }
    
    private void OnButton(object obj, ButtonEventType eventType)
    {
        switch (eventType)
        {
            case ButtonEventType.Skip:
                owner.ChangeState<MapSelectionState>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null);
        }
    }
}