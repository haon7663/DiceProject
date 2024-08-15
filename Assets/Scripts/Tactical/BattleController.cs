using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Map;
using UnityEngine;

public class BattleController : Singleton<BattleController>
{
    public Creature[] creatures;

    private PlayerData _playerData;

    [Header("- UI -")]
    public EventOptionController eventOptionController;
    public CardController cardController;
    public MapController mapController;
    public StatPanelController statPanelController;
    public DiceResultPanelController diceResultPanelController;
    public TurnOrderController turnOrderController;

    private void Start()
    {
        _playerData = DataManager.Inst.PlayerData;
        
        SetGame();
        StartCoroutine(StartTurn(creatures[0], creatures[1]));
    }

    private void SetGame()
    {
        creatures = FindObjectsByType<Creature>(FindObjectsSortMode.None);
        foreach (var creature in creatures)
        {
            creature.GetComponent<Health>().maxHp = creature.GetComponent<Health>().curHp = creature.creatureSO.hp;
            statPanelController.ConnectPanel(creature);
        }
        
        cardController.InitDeck(_playerData.cards.ToCard());
    }

    private IEnumerator StartTurn(Creature from, Creature to)
    {
        var isPlayerAtk = from.type == CreatureType.Player;
        
        turnOrderController.ShowPanel(isPlayerAtk);
        
        foreach (var creature in creatures.Where(creature => creature.type == CreatureType.Enemy))
        {
            var cards = creature.creatureSO.cards
                .Where(card => isPlayerAtk ? card.type == CardType.Defence : card.type == CardType.Attack).ToList();
            var card = cards[Random.Range(0, cards.Count)];
            cardController.CopyToPrepareCard(card);

            yield return YieldInstructionCache.WaitForSeconds(1.5f);
        }

        yield return new WaitUntil(() => CardController.Inst.playerPrepareCard);

        //주사위 굴려
        var cardSO = CardController.Inst.playerPrepareCard.cardSO;
        int diceValue = 0;
        StartCoroutine(DiceManager.Inst.RollTheDices(cardSO.effects[0].diceTypes, 0, value => diceValue = value));
        //주사위 굴려


    }
}
