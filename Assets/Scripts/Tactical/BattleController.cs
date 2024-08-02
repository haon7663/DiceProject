using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public Creature[] creatures;

    [Header("- UI -")]
    public StatPanelController statPanelController;

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
