using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using TMPro;

public enum DiceType { Four, Six, Eight, Twelve, Twenty }
public class DiceManager : MonoBehaviour
{
    public static DiceManager inst;
    private void Awake()
    {
        inst = this;
    }

    [SerializeField] private Dice dicePrefab;
    [SerializeField] private Sprite[] diceSprites;
    
    [SerializeField] private TMP_Text totalValueTMP;

    public GameObject dicePanel;
    public GameObject cardPanel;

    public int totalValue = 0;

    private List<Dice> _dices;

    public IEnumerator SpinDice(List<DiceType> diceTypes, int basicValue)
    {
        totalValueTMP.text = basicValue.ToString();
        dicePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        cardPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(99999, 0);
        
        Dice dice = null;
        totalValue = 0;

        _dices = new List<Dice>();
        for (var i = -diceTypes.Count / 2; i < (float)diceTypes.Count / 2; i++)
        {
            var diceType = diceTypes[i + diceTypes.Count / 2];
            dice = Instantiate(dicePrefab, new Vector3(1.3f * (i + (diceTypes.Count % 2 == 0 ? 0.5f : 0)) - 10, 0), Quaternion.identity);
            _dices.Add(dice); //나중ㅇ ㅔ지워
            int value = GetDiceValue(diceType);
            dice.SetUp(diceType, value);
            totalValue += value;

            yield return YieldInstructionCache.WaitForSeconds(0.25f);
        }
        
        yield return YieldInstructionCache.WaitForSeconds(1.4f);
        totalValueTMP.text = (totalValue + basicValue).ToString();
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
                Debug.LogError("없는 주사위 타입");
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

        _dices = new List<Dice>();
    }
}
