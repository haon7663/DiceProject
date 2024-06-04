using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;

public class TurnManager : MonoBehaviour
{
    public static TurnManager inst;
    private void Awake()
    {
        inst = this;
    }

    public bool playerTurn;
    private bool _actionTrigger;

    [Header("UI Header")]
    [SerializeField] private TMP_Text turnTMP;
    
    public void SetUpTurn()
    {
        StartCoroutine(TurnCoroutine());
    }
    
    private IEnumerator TurnCoroutine()
    {
        while (true)
        {
            //GameManager.Inst.player.defence = 0;
            //GameManager.Inst.enemy.defence = 0;
            
            playerTurn = true;
            turnTMP.text = "Attack Turn";
            UIManager.inst.ChangeCardPanel(true);
            
            yield return new WaitUntil(() => _actionTrigger);
            _actionTrigger = false;

            yield return YieldInstructionCache.WaitForSeconds(0.25f);
            
            playerTurn = false;
            turnTMP.text = "Defence Turn";
            UIManager.inst.ChangeCardPanel(false);
            
            yield return new WaitUntil(() => _actionTrigger);
            _actionTrigger = false;
            
            /*foreach (var creature in FindObjectsOfType<Creature>())
            {
                print("EnemyUseCard");
                var cardData = creature.creatureData.cardDatas;
                yield return StartCoroutine(creature.CardCoroutine(cardData[Random.Range(0, cardData.Count)], GameManager.inst.player.GetComponent<Health>()));
            }*/
            
            var cardData = GameManager.Inst.enemy.creatureSO.cards;
            yield return StartCoroutine(GameManager.Inst.enemy.CardCoroutine(cardData[Random.Range(0, cardData.Count)], GameManager.Inst.player));

            yield return new WaitUntil(() => _actionTrigger);
            _actionTrigger = false;
        }
    }

    public void TurnEnd()
    {
        _actionTrigger = true;
    }
}
