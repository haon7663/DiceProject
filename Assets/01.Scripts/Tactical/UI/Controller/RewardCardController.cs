using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class RewardCardController : Singleton<RewardCardController>
{
    public Panel backgroundPanel;
    
    [Header("프리팹")] 
    [SerializeField] private RewardCard cardPrefab;
    
    [Header("트랜스폼")] 
    [SerializeField] private Transform parent;

    [Header("오프셋")]
    [SerializeField] private float gap;
    
    public void ShowCards(List<CardSO> cardSOs)
    {
        backgroundPanel.SetPosition(PanelStates.Show, true);
        
        for (var i = 0; i < cardSOs.Count; i++)
        {
            var cardSO = cardSOs[i];
            
            var card = Instantiate(cardPrefab, parent);
            card.GetComponent<RectTransform>().anchoredPosition = new Vector2(i - (cardSOs.Count - 1) * 0.5f, 0) * gap;
            
            card.Init(cardSO);
            card.panel.SetPosition(PanelStates.Show, true);
            card.transform.SetAsLastSibling();

            card.OnClick += Hide;
        }
    }

    private void Hide()
    {
        backgroundPanel.SetPosition(PanelStates.Hide, true);
    }
}
