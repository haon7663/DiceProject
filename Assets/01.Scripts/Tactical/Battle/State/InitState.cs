using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Map;
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
        owner.interactionCardController.InitDeck();

        owner.player.unitSO = Resources.LoadAll<UnitSO>("Units/Player").FirstOrDefault(unit => unit.name == owner.PlayerData.name);
        if (owner.player.TryGetComponent<Health>(out var playerHealth))
        {
            playerHealth.maxHp = owner.PlayerData.MaxHealth;
            playerHealth.curHp = owner.PlayerData.Health;
            playerHealth.OnDeath += owner.DefeatEventHandler;
        }
        if (owner.player.TryGetComponent<Relic>(out var relic))
        {
            if (owner.PlayerData.Relics.Count > 0)
            {
                relic.relics = owner.PlayerData.Relics.ToRelic();
            }
        }
        if (owner.player.TryGetComponent<Act>(out var playerAct))
            playerAct.Init();
        owner.statPanelController.ConnectPanel(owner.player);
        
        switch (GameManager.Inst.currentNodeType)
        {
            case NodeType.MinorEnemy:
                owner.diceResultPanelController.ConnectPanel(owner.player);
                InitEnemy();
                yield return null;
                owner.ChangeState<PrepareBattleState>();
                break;
            case NodeType.EliteEnemy:
                owner.diceResultPanelController.ConnectPanel(owner.player);
                InitEliteEnemy();
                yield return null;
                owner.ChangeState<PrepareBattleState>();
                break;
            case NodeType.RestSite:
                InitRest();
                yield return null;
                owner.ChangeState<EventSelectionState>();
                break;
            case NodeType.Store:
                InitEvent();
                yield return null;
                owner.ChangeState<StoreState>();
                break;
            case NodeType.Boss:
                owner.diceResultPanelController.ConnectPanel(owner.player);
                InitBoss();
                yield return null;
                owner.ChangeState<PrepareBattleState>();
                break;
            case NodeType.Mystery:
                InitEvent();
                yield return null;
                owner.ChangeState<EventSelectionState>();
                break;
            case NodeType.None:
                print("None");
                yield return null;
                owner.ChangeState<MapSelectionState>();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void InitRest()
    {
        owner.eventData = Resources.Load<EventData>("Events_Rest/Rest Event");
        
        owner.eventObject.gameObject.SetActive(true);
        owner.eventObject.SetSprite(owner.eventData.eventSprite);
    }


    private void InitEvent()
    {
        owner.eventData = Resources.LoadAll<EventData>("Events").Random();
        
        owner.eventObject.gameObject.SetActive(true);
        owner.eventObject.SetSprite(owner.eventData.eventSprite);
    }
    
    private void InitBoss()
    {
        owner.enemyData = Resources.LoadAll<UnitSO>("Units/Boss").Random();
        
        owner.enemy.gameObject.SetActive(true);
        owner.enemy.unitSO = owner.enemyData;
        if (owner.enemy.TryGetComponent<Health>(out var enemyHealth))
        {
            enemyHealth.maxHp = enemyHealth.curHp = owner.enemy.unitSO.maxHp;
            enemyHealth.OnDeath += owner.VictoryEventHandler;
        }
        owner.statPanelController.ConnectPanel(owner.enemy);
        owner.diceResultPanelController.ConnectPanel(owner.enemy);
    }
    
    private void InitEliteEnemy()
    {
        owner.enemyData = Resources.LoadAll<UnitSO>("Units/EliteEnemy").Random();
        
        owner.enemy.gameObject.SetActive(true);
        owner.enemy.unitSO = owner.enemyData;
        if (owner.enemy.TryGetComponent<Health>(out var enemyHealth))
        {
            enemyHealth.maxHp = enemyHealth.curHp = owner.enemy.unitSO.maxHp;
            enemyHealth.OnDeath += owner.VictoryEventHandler;
        }
        owner.statPanelController.ConnectPanel(owner.enemy);
        owner.diceResultPanelController.ConnectPanel(owner.enemy);
    }

    private void InitEnemy()
    {
        owner.enemyData = Resources.LoadAll<UnitSO>("Units/CommonEnemy").Random();
        
        owner.enemy.gameObject.SetActive(true);
        owner.enemy.unitSO = owner.enemyData;
        if (owner.enemy.TryGetComponent<Health>(out var enemyHealth))
        {
            enemyHealth.maxHp = enemyHealth.curHp = owner.enemy.unitSO.maxHp;
            enemyHealth.OnDeath += owner.VictoryEventHandler;
        }
        owner.statPanelController.ConnectPanel(owner.enemy);
        owner.diceResultPanelController.ConnectPanel(owner.enemy);
    }
}
