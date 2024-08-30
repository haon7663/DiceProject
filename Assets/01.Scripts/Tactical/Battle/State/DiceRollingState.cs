using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceRollingState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        var diceTypes = owner.player.cardSO.cardEffects.SelectMany(cardEffect => cardEffect.diceTypes).ToList();
        StartCoroutine(Rolling(diceTypes));
    }
    
    public IEnumerator Rolling(List<DiceType> diceTypes)
    {   
        for (var i = 0; i < diceTypes.Count; i++)
        {
            var diceType = diceTypes[i];
            
            if (owner.PlayerData.dices[diceType] <= 0) continue;
            DataManager.Inst.PlayerData.dices[diceType]--;
            
            var position = new Vector3(2.5f * (i - (float)(diceTypes.Count - 1) / 2) - 10, 0) + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * 0.4f;
            var rotation = Quaternion.Euler(0, Random.Range(-15f, 15f), 0);
            
            var dice = DiceFactory.Create(diceType);
            dice.transform.position = position;
            dice.transform.rotation = rotation;
            
            yield return YieldInstructionCache.WaitForSeconds(Random.Range(0.15f, 0.225f));

            owner.diceResultPanelController.AddValue(owner.player, diceType.GetDiceValue());
        }
        
        owner.diceResultPanelController.Show();
        
        yield return YieldInstructionCache.WaitForSeconds(2f);
        
        owner.ChangeState<ActionSceneState>();
    }
}
