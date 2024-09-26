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
    

    private IEnumerator RollingDices()
    {
        owner.diceResultPanelController.Show();
        
        yield return RollDicesForUnit(owner.player);
        
        yield return RollDicesForUnit(owner.enemy);

        yield return YieldInstructionCache.WaitForSeconds(1.1f);

        StartCoroutine(DestroyDices());
        
        yield return YieldInstructionCache.WaitForSeconds(1.6f);

        owner.ChangeState<ActionSceneState>();
    }

    private IEnumerator RollDicesForUnit(Unit unit)
    {
        var behaviourInfos = unit.cardSO.behaviourInfos;
        unit.behaviourValues = new Dictionary<BehaviourInfo, int>();

        var index = 0;
        var maxIndex = behaviourInfos.SelectMany(behaviourInfo => behaviourInfo.diceTypes).Count(diceType => owner.PlayerData.Dices[diceType] > 0);
        
        owner.diceResultPanelController.SetValue(unit, 0, false);

        var displayTotalValue = 0;
        foreach (var behaviourInfo in behaviourInfos)
        {
            var totalValue = 0;
            var diceTypes = behaviourInfo.diceTypes;

            foreach (var diceType in diceTypes)
            {
                if (unit.type == UnitType.Player)
                {
                    var pos = DiceFactory.CalculateDicePosition(index++, maxIndex);
                    var diceObject = diceType.RollingDice(pos);
                    _diceObjects.Add(diceObject);
                    totalValue += diceObject.GetValue();
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
        if (unit.type == UnitType.Enemy)
        {
            owner.diceResultPanelController.SetValue(unit, displayTotalValue);
        }
    }

    private IEnumerator DestroyDices()
    {
        foreach (var dice in _diceObjects)
        {
            StartCoroutine(AddCount(dice));
            yield return YieldInstructionCache.WaitForSeconds(0.25f);
        }
        
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        
        _diceObjects.ForEach(dice => Destroy(dice.gameObject));
        _diceObjects.Clear();
    }

    private IEnumerator AddCount(DiceObject dice)
    {
        dice.Dissolve();
        
        yield return YieldInstructionCache.WaitForSeconds(0.15f);
        
        var diceCountObject = DiceFactory.InstantiatePrefab("Dices/Dice Count Movement");
        diceCountObject.transform.position = dice.transform.position;
        diceCountObject.GetComponent<DiceCountMovement>().Initialize(owner.diceResultPanelController.primaryPanel.transform.position);
        
        yield return YieldInstructionCache.WaitForSeconds(1f);
        
        owner.diceResultPanelController.AddValue(owner.player, dice.GetValue());
    }
}