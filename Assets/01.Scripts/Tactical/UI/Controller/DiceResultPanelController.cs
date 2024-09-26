using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceResultPanelController : MonoBehaviour
{
    public DiceResultPanel primaryPanel;
    public DiceResultPanel secondaryPanel;
    public DiceResultPanel topPanel;
    
    public void ConnectPanel(Unit unit)
    {
        var isPlayer = unit.type == UnitType.Player;
        var panel = isPlayer ? primaryPanel : secondaryPanel;
        panel.gameObject.SetActive(true);
        panel.Initialize(unit);
    }

    public void SetValue(Unit unit, int value, bool useDotween = true)
    {
        var isPlayer = unit.type == UnitType.Player;
        var panel = isPlayer ? primaryPanel : secondaryPanel;
        panel.SetValue(value, useDotween);
    }
    
    public void AddValue(Unit unit, int value)
    {
        SoundManager.Inst.Play("Ding");
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
        primaryPanel.GetComponent<Panel>().SetPosition(PanelStates.Hide, true);
        secondaryPanel.GetComponent<Panel>().SetPosition(PanelStates.Hide, true);
    }

    public void SetTopValue(int value)
    {
        topPanel.SetValue(value);
    }

    public void ShowTop()
    {
        topPanel.gameObject.SetActive(true);
        topPanel.GetComponent<Panel>().SetPosition(PanelStates.Show, true, 1);
    }

    public void HideTop()
    {
        topPanel.GetComponent<Panel>().SetPosition(PanelStates.Hide, true, 0.5f);
    }
}
