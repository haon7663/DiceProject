using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractionCardController : MonoBehaviour
{
    [SerializeField] private DisplayCardController displayCardController;
    
    [Header("프리팹")] 
    [SerializeField] private InteractionCard cardPrefab;

    [Header("트랜스폼")] 
    [SerializeField] private Transform canvas;
    [SerializeField] private Transform parent;
    
    private List<InteractionCard> _interactionCards;

    public void InitDeck(List<CardSO> cards)
    {
        _interactionCards = new List<InteractionCard>();

        foreach (var data in cards)
        {
            var card = Instantiate(cardPrefab, parent);
            card.Init(data);
            _interactionCards.Add(card);
            
            card.Copy += displayCardController.CopyCard;
            card.Prepare += displayCardController.PrepareCard;
        }
    }
    
    public IEnumerator ChangeDeck(bool isAttackTurn)
    {
        yield return HideCards(isAttackTurn);
        
        parent.transform.localPosition = Vector3.zero;
        
        SetCardsActive(isAttackTurn);
        
        yield return ShowCards(isAttackTurn);
    }

    private IEnumerator HideCards(bool isAttackTurn)
    {
        var cardsToHide = _interactionCards.Where(card => ShouldHideCard(card.Data.type, isAttackTurn));

        foreach (var card in cardsToHide)
        {
            card.Hide();
            yield return YieldInstructionCache.WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ShowCards(bool isAttackTurn)
    {
        var cardsToShow = _interactionCards.Where(card => ShouldShowCard(card.Data.type, isAttackTurn));

        foreach (var card in cardsToShow)
        {
            card.Show();
            yield return YieldInstructionCache.WaitForSeconds(0.1f);
        }
    }

    private void SetCardsActive(bool isAttackTurn)
    {
        foreach (var card in _interactionCards)
        { 
            card.gameObject.SetActive(ShouldShowCard(card.Data.type, isAttackTurn));
        }
    }

    private bool ShouldShowCard(CardType type, bool isAttackTurn)
    {
        return (isAttackTurn ? type == CardType.Attack : type == CardType.Defence) || type == CardType.Both;
    }
    
    private bool ShouldHideCard(CardType type, bool isAttackTurn)
    {
        return (!isAttackTurn ? type == CardType.Attack : type == CardType.Defence) || type == CardType.Both;
    }

    private List<InteractionCard> SetOrder()
    {
        return new List<InteractionCard>();
    }
}