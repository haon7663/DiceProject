using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum StatType { MaxHealth = 100, Cost = 200, GetDamage = 300, TakeDamage = 400, TakeDefence = 500, TakeRecovery = 600, }

public class Creature : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public CreatureSO creatureSO;
    public Dictionary<StatType, CreatureStat> Stats = new();
    
    private Creature _targetCreature;
    [SerializeField] private bool isPlayer;
    
    [Header("스탯")]
    public float maxHp;
    public float curHp;

    public void SetUp()
    {
        maxHp = Stats[StatType.MaxHealth].GetValue(creatureSO.hp);
        curHp = maxHp;
        
        Stats = new Dictionary<StatType, CreatureStat>
        {
            { StatType.MaxHealth, new CreatureStat() },
            { StatType.Cost, new CreatureStat() },
            { StatType.GetDamage, new CreatureStat() },
            { StatType.TakeDamage, new CreatureStat() },
            { StatType.TakeDefence, new CreatureStat() },
            { StatType.TakeRecovery, new CreatureStat() }
        };
    }
    public IEnumerator CardCoroutine(CardSO cardData, Creature target)
    {
        var value = 0;
        if (creatureSO.creatureType == CreatureType.Enemy)
        {
            //value = cardData.cardState.diceTypes.Aggregate(cardData.cardState.basicValue, (current, t) => current + DiceManager.inst.GetDiceValue(t));
        }
        else
        {
            //yield return StartCoroutine(DiceManager.inst.SpinDice(cardData.cardState.diceTypes, cardData.cardState.basicValue));
            //value = DiceManager.inst.totalValue + cardData.cardState.basicValue;
        }

        UIManager.inst.SetValue(value, isPlayer);
        
        yield return YieldInstructionCache.WaitForSeconds(0.4f);
        target.OnDamage(value);

        var typeInt = creatureSO.creatureType == CreatureType.Enemy ? 1 : -1;
        
        _spriteRenderer.sprite = creatureSO.attackSprite;
        transform.position = new Vector3(typeInt * 0.8f, 2.25f);
        transform.DOMove(new Vector3(typeInt * 0.7f, 2.25f), 1);
        
        CameraMovement.inst.VibrationForTime(0.5f);
        CameraMovement.inst.ProductionAtTime(new Vector3(typeInt * -0.6f, 0.65f, -10), typeInt * -5f, 4f);
        
        yield return YieldInstructionCache.WaitForSeconds(1.5f);
        
        _spriteRenderer.sprite = creatureSO.idleSprite;
        transform.position = new Vector3(typeInt * 1.5f, 2.25f);
        
        CameraMovement.inst.ProductionAtTime(new Vector3(0, 0, -10), 0, 5, true);
        TurnManager.inst.TurnEnd();
        
        DiceManager.inst.DestroyDices();
    }
    
    public IEnumerator DefenceCoroutine()
    {
        /*var value = 0;
        var dices = creatureData;
        
        if (creatureData.creatureType == CreatureType.Enemy)
        {
            value = dices.diceTypes.Aggregate(dices.basicValue, (current, t) => current + DiceManager.inst.GetDiceValue(t));
        }
        else
        {
            yield return StartCoroutine(DiceManager.inst.SpinDice(dices.diceTypes, dices.basicValue));
            value = DiceManager.inst.totalValue + dices.basicValue;
        }
        
        defence = value;
        UIManager.inst.SetValue(value, isPlayer);
        */
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        /*
        DiceManager.inst.dicePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(99999, 0);
        DiceManager.inst.cardPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        DiceManager.inst.DestroyDices();*/
    }
    
    public void OnDamage(float damage)
    {
        curHp -= damage;
        UIManager.inst.SetHealth(curHp, maxHp, isPlayer);
    }
}