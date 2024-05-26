using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class CardManager : MonoBehaviour
{
    public static CardManager inst;
    private void Awake()
    {
        inst = this;
    }
    
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Transform canvas;
    [SerializeField] private Transform[] cardBundles;

    public bool onCard;

    private List<Card> _cards;
    private Card _copyCard;

    private void Start()
    {
        _cards = new List<Card>();
        var cardData = GameManager.inst.playerCardDatas;
        for (var i = 0; i < cardData.Count; i++)
        {
            if (i > 8)
                return;
            var card = Instantiate(cardPrefab, cardBundles[i / 4]);
            card.SetUp(cardData[i], true);
            _cards.Add(card);
        }
    }

    public void CopyCard(CardData cardData)
    {
        _copyCard = Instantiate(cardPrefab, Camera.main.WorldToScreenPoint(new Vector3(0, 2.25f)), Quaternion.identity, canvas);
        _copyCard.SetUp(cardData, false);
        _copyCard.transform.localScale = new Vector3(0, 1.4f);
        _copyCard.transform.DOScale(Vector3.one * 1.6f, 0.5f).SetEase(Ease.OutCirc);
        onCard = true;
    }

    public void UseCard()
    {
        foreach (var card in _cards)
        {
            card.CloseUsePanel();
        }
        
        onCard = false;

        if (!_copyCard)
            return;

        _copyCard.transform.DOKill();
        _copyCard.transform.DOScale(new Vector3(1.1f, 1.1f), 0.5f).SetEase(Ease.InBack);
        Destroy(_copyCard.gameObject, 0.5f);
        _copyCard = null;
    }
    public void CancelCard()
    {
        foreach (var card in _cards)
        {
            card.CloseUsePanel();
        }
        
        onCard = false;

        if (!_copyCard)
            return;

        _copyCard.transform.DOKill();
        _copyCard.transform.DOScale(new Vector3(0, 1.4f), 0.3f).SetEase(Ease.OutCirc);
        Destroy(_copyCard.gameObject, 0.3f);
        _copyCard = null;
    }
}
