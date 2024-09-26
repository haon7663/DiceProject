using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectButton : MonoBehaviour
{
    public CharacterSelectUI characterSelectUI;
    
    public void Select()
    {
        DataManager.Inst.Generate(characterSelectUI.Current);
        Fade.Inst.FadeOut("Battle");
    }
}
