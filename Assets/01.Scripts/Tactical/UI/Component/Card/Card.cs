using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public CardSO Data { get; private set; }
    
    public Panel panel;
    
    [SerializeField] private Image cardIcon;
    [SerializeField] private Image typeIcon;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text descriptionLabel;

    public void Init(CardSO data)
    {
        Data = data;

        nameLabel.text = data.cardName;
        descriptionLabel.text = data.description;
        cardIcon.sprite = data.sprite;
        typeIcon.sprite = Resources.Load<Sprite>($"Cards/Icon/{data.type.ToString()}");
    }

    public void Show()
    {
        panel.SetPosition(PanelStates.Show, true, 0.5f);
    }

    public void Hide()
    {
        panel.SetPosition(PanelStates.Hide, true, 0.5f);
    }
}
