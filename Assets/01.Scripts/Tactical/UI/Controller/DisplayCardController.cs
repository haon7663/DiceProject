using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class DisplayCardController : MonoBehaviour
{
    public static event EventHandler<CardSO> CardPrepareEvent;

    [Header("프리팹")] 
    [SerializeField] private DisplayCard cardPrefab;
    
    [Header("트랜스폼")] 
    [SerializeField] private Transform parent;
    
    [Header("시스템")]
    public DisplayCard playerCard;
    public DisplayCard enemyCard;
    
    public void CopyCard(CardSO data, bool isPlayer)
    {
        var card = Instantiate(cardPrefab, parent);
        card.Init(data);
        card.MoveTransform(new Vector2(0, 1600));
        card.SetAnim("Show");
        card.panel.SetPosition(PanelStates.Show, true);
        
        card.transform.SetAsLastSibling();

        if (isPlayer)
        {
            if (playerCard)
                Destroy(playerCard.gameObject);
            playerCard = card;
        }
        else
        {
            if (enemyCard)
                Destroy(enemyCard.gameObject);
            enemyCard = card;
        }
    }

    public void PrepareCard(bool isPlayer)
    {
        var card = isPlayer ? playerCard : enemyCard;

        if (!card) return;

        if (isPlayer)
        {
            var diceTypes = new List<DiceType>();
            foreach (var behaviourInfo in card.Data.behaviourInfos)
            {
                diceTypes.AddRange(behaviourInfo.diceTypes);
            }
            for (var i = 0; i < (int)DiceType.Count; i++)
            {
                var diceType = (DiceType)i;
                var count = diceTypes.Count(d => d == diceType);
                if (count > 0)
                    DataManager.Inst.AddDices(diceType, -count);
            }
        }

        var sequence = DOTween.Sequence();
        sequence.Append(card.transform.DOScale(new Vector3(0.5f, 0.5f), 0.25f));
        sequence.AppendCallback(() => card.MoveTransform(new Vector3(isPlayer ? -304 : 304, 1350), true));
        sequence.AppendInterval(0.25f);
        sequence.AppendCallback(() => CardPrepareEvent?.Invoke(this, card.Data));
    }

    public void CancelCard(bool isPlayer)
    {
        if (isPlayer)
        {
            if (playerCard)
                Destroy(playerCard.gameObject);
        }
        else
        {
            if (enemyCard)
                Destroy(enemyCard.gameObject);
        }
    }

    public void Hide()
    {
        playerCard?.Hide();
        enemyCard?.Hide();
        
        Destroy(playerCard, 1f);
        Destroy(enemyCard, 1f);
    }
}