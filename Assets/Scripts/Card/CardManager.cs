using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager inst;
    private Camera _mainCamera;
    private void Awake()
    {
        inst = this;
        _mainCamera = Camera.main;
    }
    
    [Header("프리팹")]
    [SerializeField] private CardObject cardPrefab;
    
    [Header("트랜스폼")]
    [SerializeField] private Transform canvas;
    [SerializeField] private Transform[] atkCardBundles;
    [SerializeField] private Transform[] defCardBundles;

    [HideInInspector] public bool onCard;

    private List<CardObject> _cards;
    private CardObject _copyCardObject;

    private void Start()
    {
        _cards = new List<CardObject>();
        
        var attackCards = GameManager.inst.player.creatureSO.cards.FindAll(x => x.cardType == CardType.Attack);
        for (var i = 0; i < attackCards.Count; i++)
        {
            if (i > 8)
                return;
            var card = Instantiate(cardPrefab, atkCardBundles[i / 4]);
            card.SetUp(attackCards[i], true);
            _cards.Add(card);
        }
        var defenceCards = GameManager.inst.player.creatureSO.cards.FindAll(x => x.cardType == CardType.Defence);
        for (var i = 0; i < defenceCards.Count; i++)
        {
            if (i > 8)
                return;
            var card = Instantiate(cardPrefab, defCardBundles[i / 4]);
            card.SetUp(defenceCards[i], true);
            _cards.Add(card);
        }
    }

    public void CopyCard(CardSO cardData)
    {
        _copyCardObject = Instantiate(cardPrefab, _mainCamera.WorldToScreenPoint(new Vector3(0, 2.25f)), Quaternion.identity, canvas);
        _copyCardObject.SetUp(cardData, false);
        _copyCardObject.transform.localScale = new Vector3(0, 1.4f);
        _copyCardObject.transform.DOScale(Vector3.one * 1.6f, 0.5f).SetEase(Ease.OutCirc);
        onCard = true;
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

        _copyCardObject.transform.DOKill();
        _copyCardObject.transform.DOScale(new Vector3(1.1f, 1.1f), 0.5f).SetEase(Ease.InBack);
        Destroy(_copyCardObject.gameObject, 0.5f);
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

        _copyCardObject.transform.DOKill();
        _copyCardObject.transform.DOScale(new Vector3(0, 1.4f), 0.3f).SetEase(Ease.OutCirc);
        Destroy(_copyCardObject.gameObject, 0.3f);
        _copyCardObject = null;
    }
}
