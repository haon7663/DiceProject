using System;
using UnityEngine;

public enum StatusEffectStackType { Duration = 100, Intensity = 200, IntensityAndDuration = 300, Counter = 400, No = 500 }
public enum StatusEffectCalculateType { Accumulate = 100, Initialize = 200, Each = 300 }

public abstract class StatusEffectSO : ScriptableObject
{
    public new string name;
    public StatusEffectStackType statusEffectStackType = StatusEffectStackType.Duration;
    public StatusEffectCalculateType statusEffectCalculateType = StatusEffectCalculateType.Accumulate;
    
    private int _stack;
    
    public virtual void ApplyEffect(GameObject gameObject, int stack)
    {
        _stack = stack;
    }
    
    public bool DuplicateEffect(int duration)
    {
        switch (statusEffectCalculateType)
        {
            case StatusEffectCalculateType.Accumulate:
                _stack += duration;
                break;
            case StatusEffectCalculateType.Initialize:
                _stack = duration;
                break;
            case StatusEffectCalculateType.Each:
                return false;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return true;
    }

    public void UpdateCall(GameObject gameObject)
    {
        switch (statusEffectStackType)
        {
            case StatusEffectStackType.Duration:
                _stack--;
                break;
            case StatusEffectStackType.IntensityAndDuration:
                _stack--;
                break;
            case StatusEffectStackType.Intensity:
                break;
            case StatusEffectStackType.Counter:
                break;
            case StatusEffectStackType.No:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (_stack > 0)
            return;
        
        RemoveEffect(gameObject);
    }

    public virtual void UpdateEffect(GameObject gameObject)
    {
        
    }
    
    public virtual void RemoveEffect(GameObject gameObject)
    {
        _stack = 0;
    }

    public int GetCurrentStack() => _stack;
}
