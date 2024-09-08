using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Hud : MonoBehaviour
{
    [SerializeField] protected TMP_Text label;
    [SerializeField] protected Image icon;

    public virtual void Initialize(Vector2 pos, int value)
    {
        label.transform.position = pos;
    }
    
    public virtual void Initialize(Vector2 pos, int value, Sprite sprite)
    {
        label.transform.position = pos;
        icon.sprite = sprite;
    }
}
