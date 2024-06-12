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

    public IEnumerator RollTheDices(List<DiceType> diceTypes, int basicValue, Action<int> callback)
    {   
        totalValue = 0;
        UIManager.Inst.SetDiceTotalTMP(basicValue);
        
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
            
            yield return YieldInstructionCache.WaitForSeconds(0.25f);
        }

        yield return YieldInstructionCache.WaitForSeconds(1);
        UIManager.Inst.SetDiceTotalTMP(totalValue + basicValue);

        callback(totalValue + basicValue);
    }

    public int GetDicesValue(List<DiceType> diceTypes, int basicValue)
    {
        return basicValue + diceTypes.Select(GetDiceValue).Sum();
    }
    
    public int GetDiceValue(DiceType diceType)
    {
        int maxSize;
        switch (diceType)
        {
            case DiceType.Four:
                maxSize = 4;
                break;
            case DiceType.Six:
                maxSize = 6;
                break;
            case DiceType.Eight:
                maxSize = 8;
                break;
            case DiceType.Twelve:
                maxSize = 12;
                break;
            case DiceType.Twenty:
                maxSize = 20;
                break;
            default:
                return 0;
        }
        return Random.Range(1, maxSize + 1);;
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