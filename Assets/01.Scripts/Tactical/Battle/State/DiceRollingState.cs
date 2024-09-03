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

        foreach (var behaviourInfo in behaviourInfos)
        {
            var totalValue = behaviourInfo.basicValue;
            var diceTypes = behaviourInfo.diceTypes;

            foreach (var diceType in diceTypes.Where(diceType => owner.PlayerData.dices[diceType] > 0))
            {
                var pos = CalculateDicePosition(index++, maxIndex);
                totalValue += unit.type == UnitType.Player ? RollingDice(diceType, pos) : diceType.GetDiceValue();

                yield return YieldInstructionCache.WaitForSeconds(unit.type == UnitType.Player ? Random.Range(0.15f, 0.225f) : 0);
            }

            unit.behaviourValues.Add(behaviourInfo, totalValue);
            owner.diceResultPanelController.SetValue(unit, totalValue);
        }
    }

    private Vector3 CalculateDicePosition(int index, int maxIndex)
    {
        var defaultPos = new Vector3(2.5f * (index - (float)(maxIndex - 1) / 2) - 10, 0);
        var randPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * 0.4f;
        return defaultPos + randPos;
    }

    private int RollingDice(DiceType diceType, Vector3 pos)
    {
        DataManager.Inst.PlayerData.dices[diceType]--;

        var value = diceType.GetDiceValue();
        var dice = DiceFactory.Create(diceType);
        
        dice.transform.position = pos;
        dice.transform.rotation = Quaternion.Euler(0, Random.Range(-15f, 15f), 0);

        return value;
    }
}