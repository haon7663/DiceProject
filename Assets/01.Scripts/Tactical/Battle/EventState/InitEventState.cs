using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitEventState : EventState
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
        
        yield return null;
    }
}
