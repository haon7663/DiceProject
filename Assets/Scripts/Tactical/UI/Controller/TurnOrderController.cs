using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrderController : MonoBehaviour
{
    [SerializeField] private TurnOrderPanel turnOrderPanel;

    public void ShowPanel(bool isAttack)
    {
        turnOrderPanel.gameObject.SetActive(true);
        turnOrderPanel.Show(isAttack ? "공격 턴" : "방어 턴");
    }
}