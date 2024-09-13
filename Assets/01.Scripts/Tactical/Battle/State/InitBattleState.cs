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
        owner.cardController.InitDeck(owner.PlayerData.cards.ToCard());
        
        if (owner.player.TryGetComponent<Health>(out var playerHealth))
        {
            playerHealth.maxHp = playerHealth.curHp = owner.PlayerData.curHp;
            playerHealth.OnDeath += owner.DefeatEventHandler;
        }
        if (owner.player.TryGetComponent<Relic>(out var relic))
        {
            relic.relics = owner.PlayerData.relics.ToRelic();
        }
        owner.statPanelController.ConnectPanel(owner.player);
        owner.diceResultPanelController.ConnectPanel(owner.player);
        
        if (owner.enemy.TryGetComponent<Health>(out var enemyHealth))
        {
            enemyHealth.maxHp = enemyHealth.curHp = owner.enemy.unitSO.maxHp;
            enemyHealth.OnDeath += owner.VictoryEventHandler;
        }
        owner.statPanelController.ConnectPanel(owner.enemy);
        owner.diceResultPanelController.ConnectPanel(owner.enemy);
        
        yield return null;
        
        owner.ChangeState<PrepareBattleState>();
    }
}
