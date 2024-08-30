using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceRollingState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(RollingDices());
    }

    public IEnumerator RollingDices()
    {
        var unit = owner.player;
        var cardEffects = unit.cardSO.cardEffects;

        for (int i = 0; i < cardEffects.Count; i++)
        {
            
        }

        for (var i = 0; i < diceTypes.Count; i++)
        {
            var diceType = diceTypes[i];

            var defaultPos = new Vector3(2.5f * (i - (float)(diceTypes.Count - 1) / 2) - 10, 0);
            var randPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * 0.4f;
            var pos = defaultPos + randPos;
            RollingDice(diceType, pos);
        }
    }
    
    private int RollingDice(DiceType diceType, Vector3 pos)
    {
        if (owner.PlayerData.dices[diceType] <= 0) return 0;
        DataManager.Inst.PlayerData.dices[diceType]--;
        
        var value = diceType.GetDiceValue();
            
        var dice = DiceFactory.Create(diceType);
        dice.transform.position = pos;
        dice.transform.rotation = Quaternion.Euler(0, Random.Range(-15f, 15f), 0);

        return diceType.GetDiceValue();
    }
}
