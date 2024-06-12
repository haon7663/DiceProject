using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EventStageManager : Singleton<EventStageManager>
{
    [SerializeField] private EventStageSO eventStageSO;
    
    [Header("버튼")]
    [SerializeField] private EventOptionButton eventOptionButtonPrefab;
    [SerializeField] private Transform eventLayout;
    
    [Header("로그")]
    [SerializeField] private TMP_Text eventLogTextPrefab;
    [SerializeField] private Transform eventLogBundle;
    private List<TMP_Text> _eventLogTexts;
    
    private void Start()
    {
        _eventLogTexts = new List<TMP_Text>();
        
        foreach (var eventOption in eventStageSO.eventOptions)
        {
            var eventOptionButton = Instantiate(eventOptionButtonPrefab, eventLayout);
            eventOptionButton.SetUp(eventOption);
        }
        
        AddEventLog(eventStageSO.story);
    }

    public void AddEventLog(string eventLog)
    {
        var logs = eventLog.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        var sequence = DOTween.Sequence();
        
        foreach (var log in logs)
        {
            sequence.AppendCallback(() =>
            {
                var eventLogText = Instantiate(eventLogTextPrefab, eventLogBundle);
                eventLogText.text = log;
                eventLogText.rectTransform.anchoredPosition = new Vector3(0, -60);
                _eventLogTexts.Add(eventLogText);
                SetOrderLogs();
            });
            sequence.AppendInterval(1f);
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

    public IEnumerator InvokeEventOptions(EventOption eventOption)
    {
        foreach (var eventEffect in eventOption.eventEffects)
        {
            if (eventEffect.useCondition)
            {
                var value = 0;
                yield return StartCoroutine(DiceManager.Inst.RollTheDices(eventEffect.compareDiceTypes, 0, diceValue => value = diceValue));
                switch (eventEffect.compareType)
                {
                    case CompareType.Less:
                        if (value > eventEffect.compareValue)
                            yield break;
                        break;
                    case CompareType.More:
                        if (value < eventEffect.compareValue)
                            yield break;
                        break;
                    case CompareType.Same:
                        if (value != eventEffect.compareValue)
                            yield break;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            switch (eventEffect.eventEffectType)
            {
                case EventEffectType.Hp:
                    if(eventEffect.value > 0)
                        GameManager.Inst.player.OnRecovery(eventEffect.value);
                    else
                        GameManager.Inst.player.OnDamage(-eventEffect.value);
                    break;
                case EventEffectType.Dice:
                    DataManager.Inst.diceCount[eventEffect.diceType] += eventEffect.isAdd ? 1 : -1;
                    break;
                case EventEffectType.Card:
                    switch (eventEffect.cardEventType)
                    {
                        case CardEventType.Add:
                            DataManager.Inst.cardSOs.Add(eventEffect.cardSO);
                            break;
                        case CardEventType.Remove:
                            if(DataManager.Inst.cardSOs.Contains(eventEffect.cardSO))
                                DataManager.Inst.cardSOs.Remove(eventEffect.cardSO);
                            break;
                        case CardEventType.Upgrade:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            AddEventLog(eventEffect.eventLog);
        }
    }
}
