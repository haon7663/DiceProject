using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public enum StatType { MaxHealth = 100, Cost = 200, GetDamage = 300, TakeDamage = 400, TakeDefence = 500, TakeRecovery = 600, Barrier = 700 }

public class Creature : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetUp();
    }

    public CreatureSO creatureSO;
    public Dictionary<StatType, CreatureStat> Stats = new();
    
    private Creature _targetCreature;
    [SerializeField] private bool isPlayer;
    
    [Header("스탯")]
    public float maxHp;
    public float curHp;
    
    private void SetUp()
    {
        Stats = new Dictionary<StatType, CreatureStat>
        {
            { StatType.MaxHealth, new CreatureStat() },
            { StatType.Cost, new CreatureStat() },
            { StatType.GetDamage, new CreatureStat() },
            { StatType.TakeDamage, new CreatureStat() },
            { StatType.TakeDefence, new CreatureStat() },
            { StatType.TakeRecovery, new CreatureStat() },
            { StatType.Barrier, new CreatureStat() }
        };
        
        maxHp = Stats[StatType.MaxHealth].GetValue(creatureSO.hp);
        curHp = maxHp;
    }
    public IEnumerator CardCoroutine(CardSO cardSO, Creature target)
    {
        UnityEvent actionEvent;
        var value = 0;
        if (creatureSO.creatureType == CreatureType.Enemy)
        {
            value = cardSO.cardData.Sum(cardData => cardData.diceTypes.Aggregate(cardData.basicValue, (current, t) => current + DiceManager.inst.GetDiceValue(t)));
        }
        else
        {
            foreach (var cardData in cardSO.cardData)
            {
                yield return StartCoroutine(DiceManager.inst.RollTheDices(cardData.diceTypes, cardData.basicValue,
                    diceValue =>
                    {
                        switch (cardData.behaviorType)
                        {
                            case BehaviorType.Damage:
                                value += diceValue;
                                break;
                            case BehaviorType.StatusEffect:
                                target.GetComponent<StatusEffectManager>().AddEffect(cardData.statusEffectSO, diceValue);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }));
                
                UIManager.inst.SetValue(value, isPlayer);
                yield return YieldInstructionCache.WaitForSeconds(0.4f);
            }
        }
        
        yield return YieldInstructionCache.WaitForSeconds(0.4f);
        target.OnDamage(StatManager.inst.CalculateOffence(this, target, value));

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
    
    public void OnDamage(float damage)
    {
        curHp -= damage;
        UIManager.inst.SetHealth(curHp, maxHp, isPlayer);
    }
}