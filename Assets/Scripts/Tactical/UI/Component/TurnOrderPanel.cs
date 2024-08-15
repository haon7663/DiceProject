using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnOrderPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text orderLabel;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Show(string text)
    {
        _animator.SetTrigger("show");
        orderLabel.text = text;
    }
}
