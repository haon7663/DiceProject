using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class Card : MonoBehaviour
{
    [SerializeField] private Image cardIcon;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text descriptionLabel;
    [SerializeField] private GameObject usePanel;

    public CardSO Data { get; private set; }
    public bool IsPlayer { get; private set; }
    
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
        Data = data;
        IsPlayer = isPlayer;
        _useAble = useAble;

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
            StartCoroutine(CardController.Inst.PrepareCard());
        }
        else
        {
            CardController.Inst.CancelCards();
            usePanel.SetActive(true);
            CardController.Inst.CopyCard(Data, IsPlayer);
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
    
    public IEnumerator MoveTransformCoroutine(Vector2 pos, float dotweenTime = 0.25f)
    {
        yield return _rect.DOAnchorPos(pos, dotweenTime).WaitForCompletion();
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
