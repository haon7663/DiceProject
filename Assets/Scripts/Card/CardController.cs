using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CardController : Singleton<CardController>
{
    [Header("프리팹")]
    [SerializeField] private CardObject cardPrefab;
    
    [Header("트랜스폼")]
    [SerializeField] private Transform canvas;
    [SerializeField] private Transform cardParent;

    [HideInInspector] public bool onCard;
    [HideInInspector] public CardObject playerPrepareCard;
    [HideInInspector] public CardObject enemyPrepareCard;

    private List<CardObject> _cards;
    private CardObject _copyCardObject;

    public void InitDeck(List<CardSO> cards)
    {
        _cards = new List<CardObject>();
        
        var atkCards = cards.FindAll(x => x.type == CardType.Attack);
        foreach (var cardSO in atkCards)
        {
            var card = Instantiate(cardPrefab, cardParent);
            card.Init(cardSO, true);
            _cards.Add(card);
        }
        
        var defCards = cards.FindAll(x => x.type == CardType.Defence);
        foreach (var cardSO in defCards)
        {
            var card = Instantiate(cardPrefab, cardParent);
            card.Init(cardSO, true);
            _cards.Add(card);
        }
    }

    public void CopyToShowCard(CardSO cardData)
    {
        _copyCardObject = Instantiate(cardPrefab, new Vector3(0, 2.25f), Quaternion.identity, canvas);
        _copyCardObject.Init(cardData, false);
        _copyCardObject.Show();
        _copyCardObject.transform.SetAsLastSibling();
        onCard = true;
    }
    
    public void CopyToPrepareCard(CardSO cardData)
    {
        var copyCard = Instantiate(cardPrefab, new Vector3(0, 2.25f), Quaternion.identity, canvas);
        copyCard.Init(cardData, false, false);
        copyCard.Show();
        copyCard.Prepare();
        copyCard.transform.SetAsLastSibling();
    }


    public void UseCard()
    {
        foreach (var card in _cards)
        {
            card.CloseUsePanel();
        }
        
        onCard = false;

        if (!_copyCardObject)
            return;

        _copyCardObject.Prepare();
        _copyCardObject = null;
    }
    
    public void CancelCard()
    {
        foreach (var card in _cards)
        {
            card.CloseUsePanel();
        }
        
        onCard = false;

        if (!_copyCardObject)
            return;

        _copyCardObject.Close();
        _copyCardObject = null;
    }
}
