using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrderController : MonoBehaviour
{
    [SerializeField] private TurnOrderPanel turnOrderPanel;

    public IEnumerator Show(bool isAttack)
    {
        turnOrderPanel.gameObject.SetActive(true);
        yield return StartCoroutine(turnOrderPanel.Show(isAttack ? "공격 턴" : "수비 턴"));
    }
}