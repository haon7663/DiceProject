using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventChoiceButton : MonoBehaviour
{
    public event Action<EventChoice> OnExecute;

    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private GameObject activeScreen;
    
    private EventChoice _eventChoice;
    
    public void Initialize(EventChoice eventChoice)
    {
        _eventChoice = eventChoice;
        title.text = eventChoice.title;
        description.text = eventChoice.description;
        
        SetActive();
    }

    private void SetActive()
    {
        switch (_eventChoice.eventConditionType)
        {
            case EventConditionType.None:
                activeScreen.SetActive(false);
                break;
            case EventConditionType.Item:
                activeScreen.SetActive(!DataManager.Inst.playerData.Items.ToList().Any(item => item == _eventChoice.needItem.ToJson()));
                break;
            case EventConditionType.Dice:
                var isActive = false;
                foreach (var diceType in _eventChoice.needDices)
                {
                    var count = _eventChoice.needDices.Count(d => d == diceType);
                    if (DataManager.Inst.playerData.Dices[diceType] < count)
                        isActive = true;
                    print(count);
                }
                activeScreen.SetActive(isActive);
                break;
            case EventConditionType.Gold:
                activeScreen.SetActive(DataManager.Inst.playerData.Gold < _eventChoice.needGold);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Execute()
    {
        OnExecute?.Invoke(_eventChoice);
    }
}
