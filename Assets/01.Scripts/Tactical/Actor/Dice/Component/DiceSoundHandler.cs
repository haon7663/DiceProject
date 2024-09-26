using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSoundHandler : MonoBehaviour
{
    public void Play()
    {
        SoundManager.Inst.Play("DiceRoll");
    }
}
