using System;
using UnityEngine;

public enum StatusEffectStackType { Duration, Intensity, IntensityAndDuration, Counter, No }
public enum StatusEffectCalculateType { Sum, Renewal, Each }

public abstract class StatusEffectSO : ScriptableObject
{
    public new string name;
    public StatusEffectStackType statusEffectStackType;
    public StatusEffectCalculateType statusEffectCalculateType;
    
    private int _stack;
    
    public virtual void ApplyEffect(GameObject gameObject, int stack)
    {
        _stack = stack;
    }
    
    public bool DuplicateEffect(int duration)
    {
        switch (statusEffectCalculateType)
        {
            case StatusEffectCalculateType.Sum:
                _stack += duration;
                break;
            case StatusEffectCalculateType.Renewal:
                _stack = duration;
                break;
            case StatusEffectCalculateType.Each:
                return false;
            default:
                return false;
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
