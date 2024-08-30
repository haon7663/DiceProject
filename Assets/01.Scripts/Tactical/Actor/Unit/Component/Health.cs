using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public event Action<int> OnHpChanged;
    
    public int maxHp;
    public int curHp;
    
    public void OnDamage(int value)
    {
        curHp -= value;
        if (curHp <= 0)
            curHp = 0;
        OnHpChanged?.Invoke(value);
    }
    public void OnRecovery(int value)
    {
        curHp += value;
        if (curHp > maxHp)
            curHp = maxHp;
        OnHpChanged?.Invoke(value);
    }
}
