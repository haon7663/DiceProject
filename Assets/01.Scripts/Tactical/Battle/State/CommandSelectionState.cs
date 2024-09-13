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
            owner.interactionPanelController.Disable();
            StartCoroutine(ComputerTurn());
        }
        else
        {
            owner.interactionPanelController.Enable();
        }
    }
    
    private IEnumerator ComputerTurn()
    {
        var cards = Unit.unitSO.cards.Where(card => Turn.isPlayer
                    ? Unit.type == UnitType.Player ? card.type == CardType.Attack : card.type == CardType.Defence
                    : Unit.type == UnitType.Player ? card.type == CardType.Defence : card.type == CardType.Attack)
            .ToList();
        var card = cards[Random.Range(0, cards.Count)];
        owner.displayCardController.CopyCard(card, false);

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        
        owner.displayCardController.PrepareCard(false);
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        DisplayCardController.CardPrepareEvent += OnCardPrepare;
    }
    
    protected override void RemoveListeners()
    {
        base.AddListeners();
        DisplayCardController.CardPrepareEvent -= OnCardPrepare;
    }
    
    private void OnCardPrepare(object obj, CardSO data)
    {
        if (Unit.type == UnitType.Enemy)
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
}