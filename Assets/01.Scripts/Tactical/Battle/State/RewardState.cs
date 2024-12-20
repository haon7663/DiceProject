﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Map;
using UnityEngine;
using Random = UnityEngine.Random;

public class RewardState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.statPanelController.secondaryPanel.gameObject.SetActive(false);
        owner.rewardPanelController.Show(CalculateReward());
        owner.tutorialPanelController.TryToShow("전리품");
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
        switch (GameManager.Inst.currentNodeType)
        {
            case NodeType.MinorEnemy:
                break;
            case NodeType.EliteEnemy:
                break;
            case NodeType.RestSite:
                break;
            case NodeType.Store:
                break;
            case NodeType.Boss:
                break;
            case NodeType.Mystery:
                break;
            case NodeType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        var rewards = new List<Reward>();
        
        rewards.Add(new GoldReward(Random.Range(50, 101)));
        
        if (Random.value <= 0.15f)
            rewards.Add(new RelicReward(Resources.LoadAll<RelicSO>("Relics").Random()));

        var cards = Resources.LoadAll<CardSO>("Cards/Player");
        var selectCards = new List<CardSO>();
        for (var i = 0; i < 2; i++)
        {
            var card = cards.Where(c =>
                DataManager.Inst.playerData.Cards.All(card => card.name != c.cardName) &&
                selectCards.All(card => card.cardName != c.cardName)).ToList().Random();
            print($"PlayerData In Card?: {DataManager.Inst.playerData.Cards.Any(c => c.name == card.cardName)}, SelectCards In Card?: {selectCards.Any(c => c.cardName == card.cardName)}");
            
            if (card)
                selectCards.Add(card);
        }
        rewards.Add(new CardReward(selectCards));

        for (var i = 0; i < 3; i++)
        {
            if (Random.value <= 0.23f)
                rewards.Add(new ItemReward(Resources.LoadAll<ItemSO>("Items").Random()));
        }


        var count = Enumerable.Range(0, 5).Count(_ => Random.value <= 0.5f);
        if (count > 0)
            rewards.Add(new DiceReward(DiceType.Six, count));
        
        count = Enumerable.Range(0, 4).Count(_ => Random.value <= 0.3f);
        if (count > 0)
            rewards.Add(new DiceReward(DiceType.Eight, count));
        
        count = Enumerable.Range(0, 3).Count(_ => Random.value <= 0.12f);
        if (count > 0)
            rewards.Add(new DiceReward(DiceType.Twelve, count));
        
        count = Enumerable.Range(0, 2).Count(_ => Random.value <= 0.03f);
        if (count > 0)
            rewards.Add(new DiceReward(DiceType.Twenty, count));
        
        return rewards;
    }
}