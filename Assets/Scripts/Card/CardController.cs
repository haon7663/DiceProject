using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : Singleton<CardController>
{
    public static event EventHandler<CardSO> CardPrepareEvent;

    [Header("프리팹")] 
    [SerializeField] private Card cardPrefab;

    [Header("트랜스폼")] 
    [SerializeField] private Transform canvas;
    [SerializeField] private Transform cardParent;

    private List<Card> _cards;
    private Card _copyCard;

    public void InitDeck(List<CardSO> cards)
    {
        _cards = new List<Card>();

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

    public void ShowCard(CardSO data, bool isPlayer)
    {
        var copyCard = Instantiate(cardPrefab, canvas);
        copyCard.Init(data, false, isPlayer);
        copyCard.transform.SetAsLastSibling();
        copyCard.MoveTransform(new Vector2(0, 540), false);
        copyCard.SetAnimator("Show");

        _copyCard = copyCard;
    }

    public void PrepareCard()
    {
        if (!_copyCard)
            return;

        _copyCard.SetAnimator("Prepare");
        _copyCard.MoveTransform(new Vector3(_copyCard.isPlayer ? -304 : 304, 225));
        
        CardPrepareEvent?.Invoke(this, _copyCard.cardSO);
    }


    public void UseCard()
    {
        foreach (var card in _cards)
        {
            card.CloseUsePanel();
        }

        if (!_copyCard)
            return;

        _copyCard = null;
    }

    public void CancelCard()
    {
        foreach (var card in _cards)
        {
            card.CloseUsePanel();
        }

        if (!_copyCard)
            return;

        _copyCard = null;
    }
}