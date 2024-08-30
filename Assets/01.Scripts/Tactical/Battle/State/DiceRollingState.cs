using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GDX.Collections.Generic;
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
        
        unit.values = new SerializableDictionary<CardBehaviorType, int>();

        var index = 0;
        var maxIndex = cardEffects.SelectMany(cardEffect => cardEffect.diceTypes).Count();
        foreach (var cardEffect in cardEffects)
        {
            var diceTypes = cardEffect.diceTypes;
            foreach (var diceType in diceTypes)
            {
                var defaultPos = new Vector3(2.5f * (index++ - (float)(maxIndex - 1) / 2) - 10, 0);
                var randPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * 0.4f;
                var pos = defaultPos + randPos;
                
                var value = RollingDice(diceType, pos);
                unit.values.Add(cardEffect.cardBehaviorType, value);
                
                yield return YieldInstructionCache.WaitForSeconds(Random.Range(0.15f, 0.225f));
            }
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
