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
        owner.playerUnit.GetComponent<Health>().maxHp = owner.playerUnit.GetComponent<Health>().curHp = owner.PlayerData.curHp;
        owner.statPanelController.ConnectPanel(owner.playerUnit);
        
        owner.enemyUnit.GetComponent<Health>().maxHp =  owner.enemyUnit.GetComponent<Health>().curHp =  owner.enemyUnit.unitSO.maxHp;
        owner.statPanelController.ConnectPanel(owner.enemyUnit);
        yield return null;
    }

    private void SpawnUnit()
    {
        
    }
}
