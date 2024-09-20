using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public event Action<int> OnHpChanged;
    public event Action OnDeath;
    
    public int maxHp;
    public int curHp;

    private Act _act;

    private void Awake()
    {
        _act = GetComponent<Act>();
    }

    public void OnDamage(int value)
    {
        curHp -= value;
        if (curHp <= 0)
        {
            curHp = 0;
            OnDeath?.Invoke();
            _act.DeathAction(GetComponent<Unit>().unitSO.hits.Random());
        }
        _act.OnDamage();
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
