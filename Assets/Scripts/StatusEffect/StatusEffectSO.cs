using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum StatusEffectStackType { Duration = 100, Intensity = 200, IntensityAndDuration = 300, Counter = 400, No = 500 }
public enum StatusEffectCalculateType { Accumulate = 100, Initialize = 200, Each = 300 }

public abstract class StatusEffectSO : ScriptableObject
{
    public new string name;
    [TextArea] public string description;
    [FormerlySerializedAs("statusEffectSprite")] public Sprite sprite;
    
    public string label;
    
    public StatusEffectStackType statusEffectStackType = StatusEffectStackType.Duration;
    public StatusEffectCalculateType statusEffectCalculateType = StatusEffectCalculateType.Accumulate;
    
    private int _stack;
    
    public virtual void ApplyEffect(Creature creature, int stack)
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
    
    public virtual void UpdateEffect(Creature creature)
    {
        
    }

    public void UpdateStack(Creature creature)
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
        
        RemoveEffect(creature);
    }
    
    public virtual void RemoveEffect(Creature creature)
    {
        _stack = 0;
        creature.GetComponent<StatusEffect>().RemoveEffect(this);
    }

    public int GetCurrentStack() => _stack;
}
