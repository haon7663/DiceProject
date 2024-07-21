using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum DiceType { Four, Six, Eight, Twelve, Twenty }
public class DiceManager : Singleton<DiceManager>
{
    [SerializeField] private DiceObject dicePrefab;
    [SerializeField] private Sprite[] diceSprites;
    
    public int totalValue;

    private List<DiceObject> _dices = new List<DiceObject>();

    private void Start()
    {
        UIManager.Inst.SetDiceCountText();
    }

    public IEnumerator RollTheDices(List<DiceType> diceTypes, int basicValue, Action<int> callback)
    {   
        totalValue = 0;
        for (var i = 0; i < diceTypes.Count; i++)
        {
            var diceType = diceTypes[i];
            
            if (DataManager.Inst.diceCount[diceType] <= 0) continue;
            
            var dicePos = new Vector3(1.3f * (i - (float)(diceTypes.Count - 1) / 2) - 10, 0);
            var dice = Instantiate(dicePrefab, dicePos, Quaternion.identity);
            var value = GetDiceValue(diceType);
            dice.SetUp(diceType, value);
            _dices.Add(dice);
            totalValue += value;
            DataManager.Inst.diceCount[diceType]--;
            UIManager.Inst.SetDiceCountText();
            
            yield return YieldInstructionCache.WaitForSeconds(0.25f);
        }

        yield return YieldInstructionCache.WaitForSeconds(1);
        
        callback(totalValue + basicValue);
        yield break;
    }

    public int GetDicesValue(List<DiceType> diceTypes, int basicValue)
    {
        return basicValue + diceTypes.Select(GetDiceValue).Sum();
    }
    
    public int GetDiceValue(DiceType diceType)
    {
        return Random.Range(1, GetDiceMaxValue(diceType) + 1);;
    }

    public int GetDiceMaxValue(DiceType diceType)
    {
        return diceType switch
        {
            DiceType.Four => 4,
            DiceType.Six => 6,
            DiceType.Eight => 8,
            DiceType.Twelve => 12,
            DiceType.Twenty => 20,
            _ => 0
        };
    }
    
    public Sprite GetDiceSprite(DiceType diceType)
    {
        return diceSprites[(int)diceType];
    }

    public void DestroyDices()
    {
        foreach (var dice in _dices)
        {
            Destroy(dice.gameObject);
        }

        _dices = new List<DiceObject>();
    }
}