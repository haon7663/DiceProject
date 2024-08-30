using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Hud : MonoBehaviour
{
    [SerializeField] protected TMP_Text label;

    public virtual void Initialize(Vector2 pos, int value)
    {
        label.transform.position = pos;
    }
}
