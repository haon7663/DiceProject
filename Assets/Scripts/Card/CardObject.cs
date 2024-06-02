using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardObject : MonoBehaviour
{
    [SerializeField] private CardSO cardData;

    [SerializeField] private Image cardImage;
    [SerializeField] private TMP_Text nameTMP;
    [SerializeField] private TMP_Text descriptionTMP;
    [SerializeField] private GameObject usePanel;
    [SerializeField] private Image[] diceImages;

    private bool _useAble;

    public void SetUp(CardSO data, bool useAble)
    {
        cardData = data;
        _useAble = useAble;

        nameTMP.text = data.cardName;
        descriptionTMP.text = data.description;
        cardImage.sprite = data.sprite;

        /*for (var i = 0; i < data.cardState.diceTypes.Count; i++)
        {
            diceImages[i].gameObject.SetActive(true);
            diceImages[i].sprite = DiceManager.inst.GetDiceSprite(data.cardState.diceTypes[i]);
        }*/
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
            else
                CardManager.inst.CancelCard();
        }
        
        usePanel.SetActive(true);
        CardManager.inst.CopyCard(cardData);
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
        StartCoroutine(GameManager.Inst.player.CardCoroutine(cardData, GameManager.Inst.enemy));
    }
}
