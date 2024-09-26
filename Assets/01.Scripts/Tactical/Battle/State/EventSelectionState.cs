using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EventSelectionState : BattleState
{
    private List<DiceObject> _diceObjects;
    
    public override void Enter()
    {
        base.Enter();
        _diceObjects = new List<DiceObject>();
        
        owner.tutorialPanelController.TryToShow("이벤트 노드");
        
        owner.eventChoicesController.ShowEventChoices(owner.eventData);
        owner.eventChoicesController.OnExecute += Execute;
        
        owner.dialogController.Show();
        owner.dialogController.SetLogAlignment(TextAlignmentOptions.Center);
        owner.dialogController.GenerateDialog(owner.eventData.eventDescription, () => owner.interactionPanelController.Enable());
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void Execute(EventChoice eventChoice)
    {
        if (eventChoice.eventConditionType == EventConditionType.Dice)
        {
            for (var i = 0; i < (int)DiceType.Count; i++)
            {
                var diceType = (DiceType)i;
                var count = eventChoice.needDices.Count(d => d == diceType);
                if (count > 0)
                    DataManager.Inst.AddDices(diceType, -count);
            }
            StartCoroutine(RollDices(eventChoice));
        }
        else
        {
            StartCoroutine(GetReward(eventChoice, 0));
        }
    }
    
    private IEnumerator RollDices(EventChoice eventChoice)
    {
        owner.interactionPanelController.Hide();
        owner.topPanelController.Hide();
        owner.diceResultPanelController.ShowTop();

        yield return YieldInstructionCache.WaitForSeconds(1f);

        var totalValue = 0;
        var index = 0;
        var maxIndex = eventChoice.needDices.Count;
        
        foreach (var diceType in eventChoice.needDices)
        {
            var pos = DiceFactory.CalculateDicePosition(index++, maxIndex);
            var diceObject = diceType.RollingDice(pos);
            _diceObjects.Add(diceObject);
            
            totalValue += diceObject.GetValue();
            
            yield return YieldInstructionCache.WaitForSeconds(Random.Range(0.15f, 0.225f));
            
            totalValue = owner.player.Stats[StatType.Fortune].GetValue(totalValue);
        }
        
        owner.diceResultPanelController.SetTopValue(totalValue);

        yield return YieldInstructionCache.WaitForSeconds(3);
        
        if (_diceObjects.Count > 0)
        {
            _diceObjects.ForEach(dice => Destroy(dice.gameObject));
            _diceObjects.Clear();
        }
        
        owner.interactionPanelController.Disable();
        owner.topPanelController.Show();
        owner.diceResultPanelController.HideTop();
        
        yield return YieldInstructionCache.WaitForSeconds(1.5f);

        StartCoroutine(GetReward(eventChoice, totalValue));
    }

    private IEnumerator GetReward(EventChoice eventChoice, int value)
    {
        foreach (var action in eventChoice.actions)
        {
            if (action.compareInfo.IsSatisfied(value))
            {
                if (action.description == "") continue;
                owner.dialogController.GenerateDialog(action.description);
            }
        }
        
        StartCoroutine(EndEvent());

        yield return YieldInstructionCache.WaitForSeconds(0.75f);
        
        eventChoice.ExecuteActions(value);
    }

    private IEnumerator EndEvent()
    {
        yield return null;
        print("ChangeToMap");
        owner.ChangeState<MapSelectionState>();
    }
}