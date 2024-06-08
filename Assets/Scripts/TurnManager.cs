using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class TurnManager : MonoBehaviour
{
    public static TurnManager inst;
    public bool playerTurn;

    public event Action OnTurnStart;
    public event Action<bool> OnCreatureTurnStart;
    public event Action OnTurnEnd;
    public event Action OnActionComplete;

    private Creature _player;
    private Creature _enemy;
    private bool _actionTrigger;

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        _player = FindObjectsOfType<Creature>().ToList().Find(x => x.creatureSO.creatureType == CreatureType.Player);
        _enemy = FindObjectsOfType<Creature>().ToList().Find(x => x.creatureSO.creatureType == CreatureType.Enemy);
        
        SetUpTurn();
    }
    
    private void OnEnable()
    {
        OnTurnStart += ResetDefences;
        OnCreatureTurnStart += SetPlayerTurn;
        OnCreatureTurnStart += UpdateEffect;
    }
    private void OnDisable()
    {
        OnTurnStart -= ResetDefences;
        OnCreatureTurnStart -= SetPlayerTurn;
        OnCreatureTurnStart -= UpdateEffect;
    }

    public void SetUpTurn()
    {
        StartCoroutine(TurnCoroutine());
    }

    private IEnumerator TurnCoroutine()
    {
        while (true)
        {
            OnTurnStart?.Invoke();

            // Player Turn
            OnCreatureTurnStart?.Invoke(true);
            yield return PlayerTurn();

            // Enemy Turn
            OnCreatureTurnStart?.Invoke(false);
            yield return EnemyTurn();

            OnTurnEnd?.Invoke();
        }
    }

    private void ResetDefences()
    {
        _player.defence = 0;
        _enemy.defence = 0;
    }

    private void SetPlayerTurn(bool isPlayerTurn)
    {
        playerTurn = isPlayerTurn;
    }

    private void UpdateEffect(bool isPlayerTurn)
    {
        if(isPlayerTurn)
            _player.GetComponent<StatusEffectManager>().UpdateEffects();
        else
            _enemy.GetComponent<StatusEffectManager>().UpdateEffects();
    }

    private IEnumerator PlayerTurn()
    {
        yield return YieldInstructionCache.WaitForSeconds(1.5f);

        yield return PlayCard(_enemy, CardType.Defence);

        yield return new WaitUntil(() => _actionTrigger);
        _actionTrigger = false;

        yield return StartCoroutine(Action(_player, _enemy));

        yield return new WaitUntil(() => _actionTrigger);
        _actionTrigger = false;
    }

    private IEnumerator EnemyTurn()
    {
        yield return YieldInstructionCache.WaitForSeconds(1.5f);
        
        yield return PlayCard(_enemy, CardType.Attack);

        yield return new WaitUntil(() => _actionTrigger);
        _actionTrigger = false;

        yield return StartCoroutine(Action(_enemy, _player));

        yield return new WaitUntil(() => _actionTrigger);
        _actionTrigger = false;
    }

    private IEnumerator PlayCard(Creature creature, CardType cardType)
    {
        var cards = creature.creatureSO.cards.Where(x => x.cardType == cardType).ToList();
        var cardSO = cards[UnityEngine.Random.Range(0, cards.Count)];
        CardManager.inst.CopyToPrepareCard(cardSO, false);
        creature.SetCard(cardSO);
        yield break;
    }

    private IEnumerator Action(Creature attackCreature, Creature defenceCreature)
    {
        var value = 0;
        List<Tuple<StatusEffectSO, int>> saveStatusEffectSOs = new();
        var attackCardSO = attackCreature.CardSO;
        var defenceCardSO = defenceCreature.CardSO;

        foreach (var cardData in attackCardSO.cardData)
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
                            saveStatusEffectSOs.Add(new Tuple<StatusEffectSO, int>(cardData.statusEffectSO, diceValue));
                            break;
                        case BehaviorType.Defence:
                            break;
                        case BehaviorType.Avoid:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }));

            UIManager.inst.SetValue(value, attackCreature.creatureSO.creatureType == CreatureType.Player);
            yield return YieldInstructionCache.WaitForSeconds(0.4f);
        }
        
        CardManager.inst.playerPrepareCard.Use();
        CardManager.inst.enemyPrepareCard.Use();

        yield return YieldInstructionCache.WaitForSeconds(1f);
        ApplyEffects(attackCreature, defenceCreature, value, saveStatusEffectSOs);
        AnimateAction(attackCreature, defenceCreature);

        yield return YieldInstructionCache.WaitForSeconds(1.5f);

        EndAction();
    }

    private void ApplyEffects(Creature attackCreature, Creature defenceCreature, int value, List<Tuple<StatusEffectSO, int>> statusEffects)
    {
        defenceCreature.OnDamage(StatManager.inst.CalculateOffence(attackCreature, defenceCreature, value));
        foreach (var effect in statusEffects)
        {
            defenceCreature.GetComponent<StatusEffectManager>().AddEffect(effect.Item1, effect.Item2);
        }
    }

    private void AnimateAction(Creature attackCreature, Creature defenceCreature)
    {
        var sequence = DOTween.Sequence();
        
        var attackCreatureSO = attackCreature.creatureSO;
        var defenceCreatureSO = defenceCreature.creatureSO;
        
        var typeInt = attackCreatureSO.creatureType == CreatureType.Enemy ? 1 : -1;

        sequence.AppendCallback(() =>
        {
            attackCreature.SetSprite(attackCreatureSO.attackSprite);
            defenceCreature.SetSprite(defenceCreatureSO.defenceSprite);
            attackCreature.transform.position = new Vector3(typeInt * 0.8f, 2.25f);
            attackCreature.transform.DOMove(new Vector3(typeInt * 0.7f, 2.25f), 1);

            CameraMovement.inst.VibrationForTime(0.5f);
            CameraMovement.inst.ProductionAtTime(new Vector3(typeInt * -0.6f, 0.65f, -10), typeInt * -5f, 4f);
        });

        sequence.AppendInterval(1.25f);

        sequence.AppendCallback(() =>
        {
            attackCreature.SetSprite(attackCreatureSO.idleSprite);
            defenceCreature.SetSprite(defenceCreatureSO.idleSprite);
            attackCreature.transform.position = new Vector3(typeInt * 1.5f, 2.25f);

            CameraMovement.inst.ProductionAtTime(new Vector3(0, 0, -10), 0, 5, true);
        });
    }

    private void EndAction()
    {
        _actionTrigger = true;
        OnActionComplete?.Invoke();
        DiceManager.inst.DestroyDices();
    }

    public void TurnEnd()
    {
        _actionTrigger = true;
    }
}