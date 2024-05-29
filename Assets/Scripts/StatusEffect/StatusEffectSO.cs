using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffectStackType { Sum, Renewal, Each }
public abstract class StatusEffectSO : ScriptableObject
{
    public string effectName;
    public StatusEffectStackType statusEffectStackType;
    
    private int remainingDuration;
    
    public virtual void ApplyEffect(GameObject target, int duration)
    {
        remainingDuration = duration;
    }
    
    public bool DuplicateEffect(int duration)
    {
        switch (statusEffectStackType)
        {
            case StatusEffectStackType.Sum:
                remainingDuration += duration;
                break;
            case StatusEffectStackType.Renewal:
                remainingDuration = duration;
                break;
            case StatusEffectStackType.Each:
                return false;
            default:
                return false;
        }
        return true;
    }

    public void UpdateCall(GameObject target)
    {
        remainingDuration--;
    }

    public void UpdateEffect(GameObject target)
    {
        
    }
    
    public virtual void RemoveEffect(GameObject target)
    {
        remainingDuration = 0;
    }

    public int GetCurrentDuration()
    {
        return remainingDuration;
    }
}
