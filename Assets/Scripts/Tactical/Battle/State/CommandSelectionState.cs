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
    
    private IEnumerator ComputerTurn()
    {
        var cards = Unit.unitSO.cards.Where(card =>
                Turn.isPlayer
                    ? Unit.type == UnitType.Player ? card.type == CardType.Attack : card.type == CardType.Defence
                    : Unit.type == UnitType.Player ? card.type == CardType.Defence : card.type == CardType.Attack)
            .ToList();
        var card = cards[Random.Range(0, cards.Count)];
        owner.cardController.CopyToPrepareCard(card);
        
        yield return null;
        
        owner.ChangeState<UnitChangeState>();
    }
}