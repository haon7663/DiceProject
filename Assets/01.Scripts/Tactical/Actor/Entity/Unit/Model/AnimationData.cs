using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class AnimationData
{
    public string name;
    public int order;
    public Sprite actionSprite;
    public Sprite effectSprite;
    public Vector2 startOffset;
    public Vector2 endOffset;
}