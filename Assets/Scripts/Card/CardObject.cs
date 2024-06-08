using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardObject : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image cardImage;
    [SerializeField] private TMP_Text nameTMP;
    [SerializeField] private TMP_Text descriptionTMP;
    [SerializeField] private GameObject usePanel;
    [SerializeField] private Image[] diceImages;
    [SerializeField] private Animator animator;

    private CardSO _cardSO;
    private bool _useAble;
    private bool _isPlayer;
    
    private static readonly int PrepareString = Animator.StringToHash("prepare");
    private static readonly int ShowString = Animator.StringToHash("show");
    private static readonly int CloseString = Animator.StringToHash("close");
    private static readonly int UseString = Animator.StringToHash("use");

    public void SetUp(CardSO data, bool useAble, bool isPlayer = true)
    {
        _cardSO = data;
        _useAble = useAble;
        _isPlayer = isPlayer;

        nameTMP.text = data.cardName;
        descriptionTMP.text = data.description;
        cardImage.sprite = data.sprite;
    }
    
    public void OnPointClick()
    {
        if (!_useAble)
            return;

        if (CardManager.inst.onCard)
        {
            if (usePanel.activeSelf)
            {
                UseCard();
                return;
            }
            CardManager.inst.CancelCard();
        }
        
        usePanel.SetActive(true);
        CardManager.inst.CopyToShowCard(_cardSO);
    }

    public void UseCard()
    {
        if (!_useAble)
            return;
        
        CardManager.inst.UseCard();
        CardCoroutine();
        usePanel.SetActive(false);
    }

    public void CloseUsePanel() 
    {
        usePanel.SetActive(false);
    }
    
    private void CardCoroutine()
    {
        GameManager.Inst.player.SetCard(_cardSO);
    }

    public void MoveToPrepare()
    {
        rectTransform.DOAnchorPos(new Vector3(_isPlayer ? -304 : 304, 225), 0.25f).SetEase(Ease.OutSine);
        if (_isPlayer)
            CardManager.inst.playerPrepareCard = this;
        else
            CardManager.inst.enemyPrepareCard = this;
    }
    public void Use()
    {
        animator.SetTrigger(UseString);
    }
    public void Prepare()
    {
        animator.SetTrigger(PrepareString);
    }
    public void Show()
    {
        animator.SetTrigger(ShowString);
    }
    public void Close()
    {
        animator.SetTrigger(CloseString);
    }
    
    public void DestroyCard()
    {
        Destroy(gameObject);
    }
}
