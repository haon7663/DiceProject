using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InteractionCardController : MonoBehaviour
{
    [SerializeField] private DisplayCardController displayCardController;
    
    [Header("프리팹")] 
    [SerializeField] private InteractionCard cardPrefab;

    [Header("트랜스폼")] 
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private CanvasGroup canvasGroup;
    
    private List<InteractionCard> _interactionCards;
    private InteractionCard _current;

    public void InitDeck()
    {
        _interactionCards?.ForEach(c => Destroy(c.gameObject));

        _interactionCards = new List<InteractionCard>();

        foreach (var data in DataManager.Inst.playerData.Cards.ToCard())
        {
            var card = Instantiate(cardPrefab, contentRect.transform);
            card.Init(data);
            _interactionCards.Add(card);

            card.Interact += SetParentPosition;
            card.Copy += CancelCards;
            card.Copy += displayCardController.CopyCard;
            card.Prepare += displayCardController.PrepareCard;
            card.Prepare += Prepare;
        }
    }

    private void Prepare(bool isPlayer)
    {
        canvasGroup.interactable = false;
        foreach (var card in _interactionCards)
        {
            card.interactable = false;
        }
    }

    public void CancelAllCards()
    {
        foreach (var card in _interactionCards.Where(c => c.gameObject.activeSelf))
        {
            card.OnCancel();
        }
        displayCardController.CancelCard(true);
    }
    
    private void CancelCards(CardSO data, bool isPlayer)
    {
        foreach (var card in _interactionCards.Where(c => c.Data != data && c.gameObject.activeSelf))
        {
            card.OnCancel();
        }
    }
    
    public IEnumerator ChangeDeck(bool isAttackTurn)
    {
        yield return HideCards(isAttackTurn);
        
        contentRect.transform.localPosition = Vector3.zero;
        
        SetCardsActive(isAttackTurn);
        
        yield return ShowCards(isAttackTurn);

        canvasGroup.interactable = true;
        foreach (var card in _interactionCards)
        {
            card.interactable = true;
        }
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
            card.gameObject.SetActive(true);
            card.Show();
            yield return YieldInstructionCache.WaitForSeconds(0.1f);
        }
    }

    private void SetCardsActive(bool isAttackTurn)
    {
        var cardsToHide = _interactionCards.Where(card => ShouldHideCard(card.Data.type, isAttackTurn));
        foreach (var card in cardsToHide)
        {
            card.gameObject.SetActive(false);
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

    private void SetParentPosition(Card card)
    {
        var count = 0;
        for (var i = 0; i < contentRect.childCount; i++)
            if (contentRect.GetChild(i).gameObject.activeSelf)
                count++;
        if (count <= 3) return;
        
        DOTween.Kill(contentRect);
        Canvas.ForceUpdateCanvases();

        Vector2 contentPos = scrollRect.transform.InverseTransformPoint(contentRect.position);
        Vector2 cardPos = scrollRect.transform.InverseTransformPoint(card.transform.position);
        Vector2 offset = contentPos - cardPos;

        float contentMinX = -contentRect.sizeDelta.x;
        float contentMaxX = 0f;

        float targetPosX = Mathf.Clamp(offset.x + 540f, contentMinX, contentMaxX);

        contentRect.DOAnchorPosX(targetPosX, 0.25f);
    }

    private List<InteractionCard> SetOrder()
    {
        return new List<InteractionCard>();
    }
}