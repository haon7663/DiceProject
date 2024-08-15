using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPanelController : MonoBehaviour
{
    [SerializeField] private StatPanel primaryPanel;
    [SerializeField] private StatPanel secondaryPanel;

    public void ConnectPanel(Creature creature)
    {
        var isPlayer = creature.type == CreatureType.Player;
        var panel = isPlayer ? primaryPanel : secondaryPanel;
        panel.gameObject.SetActive(true);
        panel.Connect(creature);
    }
}
