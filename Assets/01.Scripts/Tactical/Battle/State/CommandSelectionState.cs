using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandSelectionState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        if (driver.Current == Drivers.Computer)
        {
            StartCoroutine(ComputerTurn());
        }
    }

    private void OnCardPrepare(object obj, CardSO data)
    {
        if (owner.CurrentUnit.type == UnitType.Enemy)
        {
            owner.enemy.cardSO = data;
            owner.ChangeState<UnitChangeState>();
        }
        else
        {
            owner.player.cardSO = data;
            owner.ChangeState<FocusActionState>();
        }
    }
    
    private IEnumerator ComputerTurn()
    {
        var cards = Unit.unitSO.cards.Where(card =>
                Turn.isPlayer
                    ? Unit.type == UnitType.Player ? card.type == CardType.Attack : card.type == CardType.Defence
                    : Unit.type == UnitType.Player ? card.type == CardType.Defence : card.type == CardType.Attack)
            .ToList();
        var card = cards[Random.Range(0, cards.Count)];
        owner.cardController.CopyCard(card, false);

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        
        StartCoroutine(owner.cardController.PrepareCard());
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        CardController.CardPrepareEvent += OnCardPrepare;
    }
    
    protected override void RemoveListeners()
    {
        base.AddListeners();
        CardController.CardPrepareEvent -= OnCardPrepare;
    }
}