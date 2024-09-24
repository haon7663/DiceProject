using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum StatusEffectStackType { Duration = 100, AfterAttack = 200, AfterHit = 300, AfterAction = 400 }
public enum StatusEffectStackDecreaseType { Minus = 100, Extinction = 200, Value = 250, None = 300 }
public enum StatusEffectCalculateType { Accumulate = 100, Initialize = 200, Each = 300 }

public abstract class StatusEffectSO : ScriptableObject
{
    public new string name;
    [TextArea] public string description;
    public Sprite sprite;
    
    public string label;
    
    public StatusEffectStackType statusEffectStackType = StatusEffectStackType.Duration;
    public StatusEffectStackDecreaseType statusEffectStackDecreaseType = StatusEffectStackDecreaseType.Minus;
    public int minusValue = 1;
    public StatusEffectCalculateType statusEffectCalculateType = StatusEffectCalculateType.Accumulate;
    
    private int _stack;
    
    public virtual void ApplyEffect(Unit unit, int stack)
    {
        _stack = stack;
    }
    
    public virtual bool DuplicateEffect(Unit unit, int stack)
    {
        switch (statusEffectCalculateType)
        {
            case StatusEffectCalculateType.Accumulate:
                _stack += stack;
                break;
            case StatusEffectCalculateType.Initialize:
                _stack = stack;
                break;
            case StatusEffectCalculateType.Each:
                return false;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return true;
    }
    
    public virtual void UpdateEffect(Unit unit)
    {
        if (statusEffectStackType == StatusEffectStackType.Duration)
        {
            UpdateStack(unit);
        }
    }

    public virtual string GetDialogString(Unit unit)
    {
        return "";
    }

    public void UpdateStack(Unit unit, int value = -1)
    {
        switch (statusEffectStackDecreaseType)
        {
            case StatusEffectStackDecreaseType.Minus:
                _stack -= minusValue;
                break;
            case StatusEffectStackDecreaseType.Extinction:
                _stack = 0;
                break;
            case StatusEffectStackDecreaseType.Value:
                _stack -= value;
                break;
            case StatusEffectStackDecreaseType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (_stack > 0)
            return;
        
        RemoveEffect(unit);
    }
    
    public virtual void RemoveEffect(Unit unit)
    {
        _stack = 0;
        unit.GetComponent<StatusEffect>().RemoveEffect(this);
    }

    public virtual int GetCurrentValue() => 0; 

    public int GetCurrentStack() => _stack;
}
