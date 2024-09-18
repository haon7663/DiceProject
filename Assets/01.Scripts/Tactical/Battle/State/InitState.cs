using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        owner.interactionCardController.InitDeck(owner.PlayerData.cards.ToCard());

        owner.player.unitSO = Resources.LoadAll<UnitSO>("Units/Player").FirstOrDefault(unit => unit.name == owner.PlayerData.name);
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

        switch (GameManager.Inst.currentGameMode)
        {
            case GameMode.Battle:
                owner.diceResultPanelController.ConnectPanel(owner.player);
                InitEnemy();
                yield return null;
                owner.ChangeState<PrepareBattleState>();
                break;
            case GameMode.Event:
                owner.eventObject.gameObject.SetActive(true);
                yield return null;
                //owner.ChangeState<PrepareBattleState>();
                break;
            case GameMode.Chest:
                break;
            case GameMode.Shop:
                break;
            case GameMode.Boss:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void InitEnemy()
    {
        owner.enemy.gameObject.SetActive(true);
        owner.enemy.unitSO = Resources.LoadAll<UnitSO>("Units/CommonEnemy").Random();
        if (owner.enemy.TryGetComponent<Health>(out var enemyHealth))
        {
            enemyHealth.maxHp = enemyHealth.curHp = owner.enemy.unitSO.maxHp;
            enemyHealth.OnDeath += owner.VictoryEventHandler;
        }
        owner.statPanelController.ConnectPanel(owner.enemy);
        owner.diceResultPanelController.ConnectPanel(owner.enemy);
    }
}
