using UnityEngine;
using System;

public abstract class Reward
{
    public abstract Sprite GetSprite();

    public abstract string GetLabel();

    public virtual Vector2 GetSizeDelta()
    {
        return Vector2.one * 100;
    }
    
    public abstract void Execute();
}