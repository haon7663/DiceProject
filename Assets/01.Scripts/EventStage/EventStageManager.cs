using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EventStageManager : Singleton<EventStageManager>
{
    /*[FormerlySerializedAs("eventStageSO")] [SerializeField] private EventSO eventSO;
    
    [Header("버튼")]
    [SerializeField] private EventOptionButton eventOptionButtonPrefab;
    [SerializeField] private Transform eventLayout;
    private List<EventOptionButton> _eventOptionButtons;
    
    [Header("로그")]
    [SerializeField] private TMP_Text eventLogTextPrefab;
    [SerializeField] private Transform eventLogBundle;
    private List<TMP_Text> _eventLogTexts;

    [Header("오브젝트")]
    [SerializeField] private SpriteRenderer eventSpriteRenderer;

    [SerializeField] private Sprite evenqr;
    
    private Sequence _sequence;
    private void Start()
    {
        if (GameManager.Inst.currentGameMode != GameMode.Event)
            return;
        
        _eventLogTexts = new List<TMP_Text>();
        _eventOptionButtons = new List<EventOptionButton>();
        
        foreach (var eventOption in eventSO.eventOptions)
        {
            var eventOptionButton = Instantiate(eventOptionButtonPrefab, eventLayout);
            eventOptionButton.Init(eventOption);
            _eventOptionButtons.Add(eventOptionButton);
        }

        eventSpriteRenderer.sprite = eventSO.eventSprite;
        
        AddEventLog(eventSO.story);
    }

    public void AddEventLog(string eventLog)
    {
        var logs = eventLog.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        
        _sequence = DOTween.Sequence().SetAutoKill(false);
        foreach (var log in logs)
        {
            _sequence.AppendCallback(() =>
            {
                var eventLogText = Instantiate(eventLogTextPrefab, eventLogBundle);
                eventLogText.text = log;
                eventLogText.rectTransform.anchoredPosition = new Vector3(0, -60);
                _eventLogTexts.Add(eventLogText);
                SetOrderLogs();
            });
            _sequence.AppendInterval(1f);
        }
    }

    private void SetOrderLogs()
    {
        for (var i = 0; i < _eventLogTexts.Count; i++)
        {
            _eventLogTexts[i].rectTransform.DOAnchorPosY((_eventLogTexts.Count - 1) * 60 - i * 60, 0.3f).SetEase(Ease.OutQuad);
        }
        for (var i = 0; i < _eventLogTexts.Count; i++)
        {
            if (i <= 1) continue;
            var index = _eventLogTexts.Count - 1 - i;
            _eventLogTexts[index].DOFade(0.25f, 0.3f);
            if (i <= 2) continue;
            _eventLogTexts[index].DOFade(0, 0.3f);
            _eventLogTexts.RemoveAt(index);
        }
    }

    private void CloseEventOptionButtons()
    {
        for (var i = _eventOptionButtons.Count - 1; i >= 0; i--)
        {
            _eventOptionButtons[i].gameObject.SetActive(false);
        }
    }

    public void InvokeEvent(EventOption eventOption)
    {
        StartCoroutine(InvokeEventOptions(eventOption));
    }

    public IEnumerator InvokeEventOptions(EventOption eventOption)
    {
        CloseEventOptionButtons();
        
        var diceValue = 0;
        if (eventOption.useCondition)
        {
            yield return null;
            /*yield return StartCoroutine(DiceManager.Inst.RollTheDices(eventOption.diceTypes, 0,
                value => diceValue = value));#1#
        }
        foreach (var eventEffect in eventOption.eventEffects)
        {
            if (eventOption.useCondition)
            {
                if(!eventEffect.compareInfo.compareType.IsSatisfied(diceValue, eventEffect.compareInfo.value))
                    continue;
            }

            eventSpriteRenderer.sprite = evenqr;
            AddEventLog(eventEffect.eventLog);
            switch (eventEffect.eventEffectType)
            {
                case EventEffectType.Hp:
                    /*if(eventEffect.value > 0)
                        GameManager.Inst.player.OnRecovery(eventEffect.value);
                    else
                        GameManager.Inst.player.OnDamage(-eventEffect.value);#1#
                    break;
                case EventEffectType.Dice:
                    DataManager.Inst.playerData.dices[eventEffect.diceType] += eventEffect.isAdd ? 1 : -1;
                    break;
                case EventEffectType.Card:
                    switch (eventEffect.cardEventType)
                    {
                        case CardEventType.Add:
                            DataManager.Inst.playerData.cards.Add(eventEffect.cardSO.ToJson());
                            break;
                        case CardEventType.Remove:
                            if(DataManager.Inst.playerData.cards.Contains(eventEffect.cardSO.ToJson()))
                                DataManager.Inst.playerData.cards.Remove(eventEffect.cardSO.ToJson());
                            break;
                        case CardEventType.Upgrade:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case EventEffectType.Relic:
                    RelicManager.Inst.AddRelic(eventEffect.relicSO);
                    break;
                case EventEffectType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }*/
}
