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
        if (owner.player.TryGetComponent<Health>(out var playerHealth))
            playerHealth.maxHp = playerHealth.curHp = owner.PlayerData.curHp;
        if (owner.player.TryGetComponent<Relic>(out var relic))
            relic.relics = owner.PlayerData.relics.ToRelic();
        owner.statPanelController.ConnectPanel(owner.player);
        owner.diceResultPanelController.ConnectPanel(owner.player);
        
        if (owner.enemy.TryGetComponent<Health>(out var enemyHealth))
            enemyHealth.maxHp = enemyHealth.curHp = owner.enemy.unitSO.maxHp;
        owner.statPanelController.ConnectPanel(owner.enemy);
        owner.diceResultPanelController.ConnectPanel(owner.enemy);
        
        owner.cardController.InitDeck(owner.PlayerData.cards.ToCard());
        
        yield return null;
        
        owner.ChangeState<TurnChangeState>();
    }
}
