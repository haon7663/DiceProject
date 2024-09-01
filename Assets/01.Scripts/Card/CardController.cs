using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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
    private List<Card> _copyCards;
    private Card _copyCard;

    public void InitDeck(List<CardSO> cards)
    {
        _cards = new List<Card>();
        _copyCards = new List<Card>();

        foreach (var data in cards)
        {
            var card = Instantiate(cardPrefab, cardParent);
            card.Init(data, true);
            _cards.Add(card);
        }
    }

    public void ChangeCardsActive(bool attack)
    {
        foreach (var card in _cards)
        {
            var type = card.Data.type;
            var isActive = (attack ? type == CardType.Attack : type == CardType.Defence)
                    || type == CardType.Both;
            
            card.gameObject.SetActive(isActive);
        }
    }

    public void CopyCard(CardSO data, bool isPlayer)
    {
        var copyCard = Instantiate(cardPrefab, canvas);
        copyCard.Init(data, false, isPlayer);
        copyCard.transform.SetAsLastSibling();
        copyCard.MoveTransform(new Vector2(0, 540), false);
        copyCard.SetAnimator("Show");
        
        _copyCards.Add(copyCard);
        _copyCard = copyCard;
    }

    public IEnumerator PrepareCard()
    {
        if (!_copyCard)
            yield break;

        yield return _copyCard.transform.DOScale(new Vector3(0.5f, 0.5f), 0.25f).WaitForCompletion();
        yield return StartCoroutine(_copyCard.MoveTransformCoroutine(new Vector3(_copyCard.IsPlayer ? -304 : 304, 150)));
        
        CardPrepareEvent?.Invoke(this, _copyCard.Data);
    }


    public void UseCards()
    {
        foreach (var copyCard in _copyCards)
        {
            copyCard.SetAnimator("Use");
        }
        _copyCards.Clear();
        _copyCard = null;
    }

    public void CancelCards()
    {
        foreach (var card in _cards)
        {
            card.CloseUsePanel();
        }

        if (!_copyCard || !_copyCard.IsPlayer)
            return;
        
        _copyCards.Remove(_copyCard);
        Destroy(_copyCard.gameObject);
        _copyCard = null;
    }
}