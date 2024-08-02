using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public event Action OnHpChanged;
    
    public int maxHp;
    public int curHp;
    
    public void OnDamage(int damage)
    {
        curHp -= damage;
        if (curHp <= 0)
            curHp = 0;
        UIManager.Inst.PopDamageText(transform.position, damage);
        OnHpChanged?.Invoke();
    }
    public void OnRecovery(int recovery)
    {
        curHp += recovery;
        if (curHp > maxHp)
            curHp = maxHp;
        UIManager.Inst.PopRecoveryText(transform.position, recovery);
        OnHpChanged?.Invoke();
    }
}
