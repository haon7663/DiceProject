using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private Image cardIcon;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text descriptionLabel;
    [SerializeField] private GameObject usePanel;

    public CardSO cardSO;
    public bool isPlayer;
    
    private bool _useAble;
    
    private RectTransform _rect;
    private Animator _animator;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _animator = GetComponent<Animator>();
    }

    public void Init(CardSO data, bool useAble, bool isPlayer = true)
    {
        cardSO = data;
        _useAble = useAble;
        this.isPlayer = isPlayer;

        nameLabel.text = data.cardName;
        descriptionLabel.text = data.description;
        cardIcon.sprite = data.sprite;
    }
    
    public void OnPointClick()
    {
        if (!_useAble)
            return;
        
        if (usePanel.activeSelf)
        {
            usePanel.SetActive(false);
            CardController.Inst.PrepareCard();
        }
        else
        {
            usePanel.SetActive(true);
            CardController.Inst.ShowCard(cardSO, isPlayer);
        }
    }

    public void MoveTransform(Vector2 pos, bool useDotween = true, float dotweenTime = 0.25f)
    {
        if (useDotween)
        {
            _rect.DOAnchorPos(pos, dotweenTime);
        }
        else
        {
            _rect.anchoredPosition = pos;
        }
    }

    public void SetAnimator(string animName)
    {
        _animator.SetTrigger(animName);
    }

    public void CloseUsePanel() 
    {
        usePanel.SetActive(false);
    }
}
