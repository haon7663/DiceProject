using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Hud : MonoBehaviour
{
    [SerializeField] protected TMP_Text label;

    protected RectTransform rect;
    
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    
    public virtual void Initialize(Vector2 pos)
    {
        transform.position = pos;
    }

    public virtual void Initialize(Vector2 pos, int value)
    {
        transform.position = pos;
    }
    
    public virtual void Initialize(Vector2 pos, int value, Sprite sprite)
    {
        transform.position = pos;
    }
}
