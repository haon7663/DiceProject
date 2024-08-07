using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class CardObject : MonoBehaviour
{
    [SerializeField] private Image cardIcon;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text descriptionLabel;
    [SerializeField] private GameObject usePanel;

    private RectTransform _rect;
    private Animator _animator;

    private CardSO _cardSO;
    private bool _useAble;
    private bool _isPlayer;
    
    private static readonly int PrepareString = Animator.StringToHash("prepare");
    private static readonly int ShowString = Animator.StringToHash("show");
    private static readonly int CloseString = Animator.StringToHash("close");
    private static readonly int UseString = Animator.StringToHash("use");

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _animator = GetComponent<Animator>();
    }

    public void SetUp(CardSO data, bool useAble, bool isPlayer = true)
    {
        _cardSO = data;
        _useAble = useAble;
        _isPlayer = isPlayer;

        nameLabel.text = data.cardName;
        descriptionLabel.text = data.description;
        cardIcon.sprite = data.sprite;
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
        _rect.DOAnchorPos(new Vector3(_isPlayer ? -304 : 304, 225), 0.25f).SetEase(Ease.OutSine);
        if (_isPlayer)
            CardManager.inst.playerPrepareCard = this;
        else
            CardManager.inst.enemyPrepareCard = this;
    }
    public void Use()
    {
        _animator.SetTrigger(UseString);
    }
    public void Prepare()
    {
        _animator.SetTrigger(PrepareString);
    }
    public void Show()
    {
        _animator.SetTrigger(ShowString);
    }
    public void Close()
    {
        _animator.SetTrigger(CloseString);
    }
    
    public void DestroyCard()
    {
        Destroy(gameObject);
    }
}
