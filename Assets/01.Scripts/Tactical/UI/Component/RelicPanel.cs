using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicPanel : MonoBehaviour
{
    public RelicSO Data { get; private set; }

    public Button button;
    
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text label;
    
    public void Initialize(RelicSO relicSO)
    {
        Data = relicSO;

        icon.sprite = relicSO.sprite;
        label.text = relicSO.name;
        
        button.onClick.AddListener(() => BattleController.Inst.itemPanel.Initialize(Data));
    }
}
