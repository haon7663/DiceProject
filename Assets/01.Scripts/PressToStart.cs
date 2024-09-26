using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressToStart : MonoBehaviour
{
    private bool _onClick;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_onClick)
        {
            Fade.Inst.FadeOut("CharacterSelection");
            _onClick = true;
        }
    }
}
