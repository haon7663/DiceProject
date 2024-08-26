using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceResultPanelController : MonoBehaviour
{
    [SerializeField] private DiceResultPanel primaryPanel;
    [SerializeField] private DiceResultPanel secondaryPanel;
    [SerializeField] private DiceResultPanel topPanel;
    
    public void ConnectPanel(Unit unit)
    {
        var isPlayer = unit.type == UnitType.Player;
        var panel = isPlayer ? primaryPanel : secondaryPanel;
        panel.gameObject.SetActive(true);
    }
    
    public void ShowTopPanel()
    {
        topPanel.gameObject.SetActive(true);
    }
}
