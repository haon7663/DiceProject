using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        owner.player.GetComponent<Health>().maxHp = owner.player.GetComponent<Health>().curHp = owner.PlayerData.curHp;
        owner.statPanelController.ConnectPanel(owner.player);
        owner.diceResultPanelController.ConnectPanel(owner.player);
        
        owner.enemy.GetComponent<Health>().maxHp = owner.enemy.GetComponent<Health>().curHp = owner.enemy.unitSO.maxHp;
        owner.statPanelController.ConnectPanel(owner.enemy);
        owner.diceResultPanelController.ConnectPanel(owner.enemy);
        
        owner.cardController.InitDeck(owner.PlayerData.cards.ToCard());
        
        yield return null;
        
        owner.ChangeState<TurnChangeState>();
    }
}
