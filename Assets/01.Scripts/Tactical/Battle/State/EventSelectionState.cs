﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventSelectionState : BattleState
{
    private List<DiceObject> _diceObjects;
    
    public override void Enter()
    {
        base.Enter();
        _diceObjects = new List<DiceObject>();
        owner.eventChoicesController.ShowEventChoices(owner.eventData);
        owner.eventChoicesController.OnExecute += Execute;
        
        owner.dialogController.GenerateDialog(owner.eventData.eventDescription);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void Execute(EventChoice eventChoice)
    {
        if (eventChoice.eventConditionType == EventConditionType.Dice)
        {
            StartCoroutine(RollDices(eventChoice));
        }
        else
        {
            GetReward(eventChoice, 0);
        }
    }
    
    private IEnumerator RollDices(EventChoice eventChoice)
    {
        owner.interactionPanelController.Hide();
        owner.topPanelController.Hide();
        owner.diceResultPanelController.ShowTop();
        
        yield return YieldInstructionCache.WaitForSeconds(1f);
        
        var index = 0;
        var maxIndex = eventChoice.needDices.Count(diceType => owner.PlayerData.Dices[diceType] > 0);

        var totalValue = 0;
        
        foreach (var diceType in eventChoice.needDices)
        {
            if (owner.PlayerData.Dices[diceType] <= 0) continue;
                    
            var pos = DiceFactory.CalculateDicePosition(index++, maxIndex);
            var diceObject = diceType.RollingDice(pos);
            _diceObjects.Add(diceObject);
            
            totalValue += diceObject.GetValue();
            
            yield return YieldInstructionCache.WaitForSeconds(Random.Range(0.15f, 0.225f));
            
            Debug.Log($"기존 주사위 값: {totalValue}, 행운 보너스: {owner.player.Stats[StatType.Fortune].GetValue(totalValue)}");
            totalValue = owner.player.Stats[StatType.Fortune].GetValue(totalValue);
        }
        
        owner.diceResultPanelController.SetTopValue(totalValue);

        yield return YieldInstructionCache.WaitForSeconds(3);
        
        if (_diceObjects.Count > 0)
        {
            _diceObjects.ForEach(dice => Destroy(dice.gameObject));
            _diceObjects.Clear();
        }
        
        owner.interactionPanelController.Show();
        owner.topPanelController.Show();
        owner.diceResultPanelController.HideTop();
        
        yield return YieldInstructionCache.WaitForSeconds(1.5f);

        GetReward(eventChoice, totalValue);
    }

    private void GetReward(EventChoice eventChoice, int value)
    {
        foreach (var action in eventChoice.actions)
        {
            if (action.compareInfo.IsSatisfied(value))
            {
                if (action.description == "") continue;
                owner.dialogController.GenerateDialog(action.description);
            }
        }
        eventChoice.ExecuteActions(value);
        EndEvent();
    }

    private void EndEvent()
    {
        owner.ChangeState<MapSelectionState>();
    }
}