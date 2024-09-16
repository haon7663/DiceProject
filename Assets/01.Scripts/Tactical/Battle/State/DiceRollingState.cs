using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GDX.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceRollingState : BattleState
{
    private List<DiceObject> _diceObjects;
    
    public override void Enter()
    {
        base.Enter();
        _diceObjects = new List<DiceObject>();
        StartCoroutine(RollingDices());
    }
    public override void Exit()
    {
        base.Exit();
        _diceObjects.ForEach(dice => Destroy(dice.gameObject));
        _diceObjects.Clear();
    }
    

    private IEnumerator RollingDices()
    {
        owner.diceResultPanelController.Show();
        
        yield return RollDicesForUnit(owner.player);
        
        yield return RollDicesForUnit(owner.enemy);

        yield return YieldInstructionCache.WaitForSeconds(1.5f);

        owner.ChangeState<ActionSceneState>();
    }

    private IEnumerator RollDicesForUnit(Unit unit)
    {
        var behaviourInfos = unit.cardSO.behaviourInfos;
        unit.behaviourValues = new Dictionary<BehaviourInfo, int>();

        var index = 0;
        var maxIndex = behaviourInfos.SelectMany(behaviourInfo => behaviourInfo.diceTypes).Count(diceType => owner.PlayerData.dices[diceType] > 0);

        var displayTotalValue = 0;
        foreach (var behaviourInfo in behaviourInfos)
        {
            var totalValue = 0;
            var diceTypes = behaviourInfo.diceTypes;

            foreach (var diceType in diceTypes)
            {
                if (unit.type == UnitType.Player)
                {
                    if (owner.PlayerData.dices[diceType] <= 0) continue;
                    
                    var pos = CalculateDicePosition(index++, maxIndex);
                    totalValue += RollingDice(diceType, pos);
                    yield return YieldInstructionCache.WaitForSeconds(Random.Range(0.15f, 0.225f));
                }
                else
                {
                    totalValue += diceType.GetDiceValue();
                }
                Debug.Log($"기존 주사위 값: {totalValue}, 행운 보너스: {unit.Stats[StatType.Fortune].GetValue(totalValue)}");
                totalValue = unit.Stats[StatType.Fortune].GetValue(totalValue);
            }
            
            unit.behaviourValues.Add(behaviourInfo, totalValue);
            displayTotalValue += totalValue;
        }
        owner.diceResultPanelController.SetValue(unit, displayTotalValue);
    }

    private Vector3 CalculateDicePosition(int index, int maxIndex)
    {
        var defaultPos = new Vector3(2.5f * (index - (float)(maxIndex - 1) / 2) - 10, 0);
        var randPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * 0.4f;
        return defaultPos + randPos;
    }

    private int RollingDice(DiceType diceType, Vector3 pos)
    {
        DataManager.Inst.playerData.dices[diceType]--;
        owner.diceCountPanelController.UpdateCount();
        
        var dice = DiceFactory.Create(diceType);
        dice.transform.position = pos;
        dice.transform.rotation = Quaternion.Euler(0, Random.Range(-15f, 15f), 0);

        var value = diceType.GetDiceValue();
        var diceObject = dice.GetComponent<DiceObject>();
        diceObject.Initialize(value);
        _diceObjects.Add(diceObject);

        return value;
    }
}