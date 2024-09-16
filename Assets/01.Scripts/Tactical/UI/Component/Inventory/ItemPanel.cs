using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public Panel panel;
    
    public ItemSO Data { get; private set; }

    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text description;
    
    public void Initialize(ItemSO itemSO)
    {
        Data = itemSO;
        
        icon.sprite = itemSO.sprite;
        nameLabel.text = itemSO.itemName;
        description.text = itemSO.description;
    }

    public void Use()
    {
        if (!Data) return;
    }
}