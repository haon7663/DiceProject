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
        panel.Initialize(unit);
    }

    public void AddValue(Unit unit, int value)
    {
        var isPlayer = unit.type == UnitType.Player;
        var panel = isPlayer ? primaryPanel : secondaryPanel;
        panel.AddValue(value);
    }

    public void Show()
    {
        primaryPanel.GetComponent<Panel>().SetPosition(PanelStates.Show, true, 1);
        secondaryPanel.GetComponent<Panel>().SetPosition(PanelStates.Show, true, 1);
    }
    
    public void Hide()
    {
        primaryPanel.GetComponent<Panel>().SetPosition(PanelStates.Hide, true, 0.5f);
        secondaryPanel.GetComponent<Panel>().SetPosition(PanelStates.Hide, true, 0.5f);
    }
}
