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
        while (GameManager.inst.onPlaying)
        {
            GameManager.inst.player.defence = 0;
            GameManager.inst.enemy.defence = 0;
            
            playerTurn = true;
            turnTMP.text = "Player Turn";
            
            yield return StartCoroutine(GameManager.inst.enemy.DefenceCoroutine());
            yield return new WaitUntil(() => _actionTrigger);
            _actionTrigger = false;

            playerTurn = false;
            turnTMP.text = "Enemy Turn";

            yield return YieldInstructionCache.WaitForSeconds(0.25f);

            /*foreach (var creature in FindObjectsOfType<Creature>())
            {
                print("EnemyUseCard");
                var cardData = creature.creatureData.cardDatas;
                yield return StartCoroutine(creature.CardCoroutine(cardData[Random.Range(0, cardData.Count)], GameManager.inst.player.GetComponent<Health>()));
            }*/
            
            var cardData = GameManager.inst.enemy.creatureData.cardDatas;
            yield return StartCoroutine(GameManager.inst.enemy.CardCoroutine(cardData[Random.Range(0, cardData.Count)], GameManager.inst.player));

            yield return new WaitUntil(() => _actionTrigger);
            _actionTrigger = false;
        }
    }

    public void TurnEnd()
    {
        _actionTrigger = true;
    }
}
