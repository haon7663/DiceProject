using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using TMPro;
using Random = UnityEngine.Random;

public enum DiceType { Four, Six, Eight, Twelve, Twenty }
public class DiceManager : MonoBehaviour
{
    public static DiceManager inst;
    private void Awake()
    {
        inst = this;
    }

    [SerializeField] private DiceObject dicePrefab;
    [SerializeField] private Sprite[] diceSprites;
    

    public int totalValue = 0;

    private List<DiceObject> _dices = new List<DiceObject>();

    public IEnumerator SpinDice(List<DiceType> diceTypes, int basicValue)
    {   
        totalValue = 0;
        UIManager.inst.SetDiceTotalTMP(basicValue);

        var diceAndValue = new Dictionary<int, int>();
        for (var i = 0; i < diceTypes.Count; i++)
        {
            diceAndValue.Add(i, GetDiceValue(diceType));
        }
        
        for (var i = -diceTypes.Count / 2; i < (float)diceTypes.Count / 2; i++)
        {
            var diceType = diceTypes[i + diceTypes.Count / 2];
            var dice = Instantiate(dicePrefab, new Vector3(1.3f * (i + (diceTypes.Count % 2 == 0 ? 0.5f : 0)) - 10, 0), Quaternion.identity);
            _dices.Add(dice); //나중ㅇ ㅔ지워
            var value = GetDiceValue(diceType);
            dice.SetUp(diceType, value);
            totalValue += value;

            yield return YieldInstructionCache.WaitForSeconds(0.25f);
        }
        
        yield return YieldInstructionCache.WaitForSeconds(1.4f);
        UIManager.inst.SetDiceTotalTMP(totalValue + basicValue);
        yield return YieldInstructionCache.WaitForSeconds(1.2f);
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