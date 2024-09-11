using UnityEngine;

public abstract class Reward
{
    public abstract Sprite GetSprite();

    public abstract string GetLabel();
    
    public abstract void Execute();
}