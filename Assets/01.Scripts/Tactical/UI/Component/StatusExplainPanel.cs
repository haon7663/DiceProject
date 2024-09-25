using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusExplainPanel : MonoBehaviour
{
    public Panel panel;

    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text descriptionLabel;

    public void Initialize(StatusEffectSO statusEffect)
    {
        
    }
}