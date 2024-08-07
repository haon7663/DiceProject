using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Map;
using UnityEngine;

public class BattleController : Singleton<BattleController>
{
    public Creature[] creatures;

    public EventOptionController eventOptionController;
    public MapController mapController;
    public StatPanelController statPanelController;
    public DiceResultPanelController diceResultPanelController;
    public TurnOrderController turnOrderController;

    private void Start()
    {
        creatures = FindObjectsByType<Creature>(FindObjectsSortMode.None);
        foreach (var creature in creatures)
        {
            creature.GetComponent<Health>().maxHp = creature.GetComponent<Health>().curHp = creature.creatureSO.hp;
            statPanelController.ConnectPanel(creature);
        }
    }
}
