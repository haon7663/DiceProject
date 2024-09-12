using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RewardState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.rewardPanelController.Show(CalculateReward());
    }

    public override void Exit()
    {
        base.Exit();
        owner.rewardPanelController.Hide();
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

    private List<Reward> CalculateReward() 
    {
        var rewards = new List<Reward>();
        
        rewards.Add(new GoldReward(Random.Range(50, 101)));
        
        if (Random.value <= 0.15f)
            rewards.Add(new RelicReward(Resources.LoadAll<RelicSO>("Relics").Random()));
        
        if (Random.value <= 0.40f)
            rewards.Add(new CardReward());

        var count = Enumerable.Range(0, 5).Count(_ => Random.value <= 0.75f);
        if (count > 0)
            rewards.Add(new DiceReward(DiceType.Six, count));
        
        count = Enumerable.Range(0, 4).Count(_ => Random.value <= 0.5f);
        if (count > 0)
            rewards.Add(new DiceReward(DiceType.Eight, count));
        
        count = Enumerable.Range(0, 3).Count(_ => Random.value <= 0.25f);
        if (count > 0)
            rewards.Add(new DiceReward(DiceType.Twelve, count));
        
        count = Enumerable.Range(0, 2).Count(_ => Random.value <= 0.05f);
        if (count > 0)
            rewards.Add(new DiceReward(DiceType.Twenty, count));
        
        return rewards;
    }
}