using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventSelectionState : BattleState
{
    private List<DiceObject> _diceObjects;
    
    public override void Enter()
    {
        base.Enter();
        owner.eventChoicesController.ShowEventChoices(owner.eventData);
        owner.eventChoicesController.OnExecute += Execute;
    }

    public override void Exit()
    {
        base.Exit();
        _diceObjects.ForEach(dice => Destroy(dice.gameObject));
        _diceObjects.Clear();
    }

    private void Execute(EventChoice eventChoice)
    {
        if (eventChoice.needDices.Count > 0)
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
        
        yield return YieldInstructionCache.WaitForSeconds(1f);
        
        var index = 0;
        var maxIndex = eventChoice.needDices.Count(diceType => owner.PlayerData.Dices[diceType] > 0);

        var totalValue = 0;
        
        _diceObjects = new List<DiceObject>();
        
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

        yield return YieldInstructionCache.WaitForSeconds(1);
        
        owner.interactionPanelController.Show();
        owner.topPanelController.Show();

        GetReward(eventChoice, totalValue);
    }

    private void GetReward(EventChoice eventChoice, int value)
    {
        eventChoice.ExecuteActions(value);
        EndEvent();
    }

    private void EndEvent()
    {
        owner.ChangeState<MapSelectionState>();
    }
}