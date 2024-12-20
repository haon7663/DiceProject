using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPanelController : MonoBehaviour
{
    public StatPanel primaryPanel;
    public StatPanel secondaryPanel;

    public void ConnectPanel(Unit unit)
    {
        var isPlayer = unit.type == UnitType.Player;
        var panel = isPlayer ? primaryPanel : secondaryPanel;
        panel.gameObject.SetActive(true);
        panel.Connect(unit);
    }
}
