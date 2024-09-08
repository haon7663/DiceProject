using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(fileName = "RelicSO", menuName = "Scriptable Object/RelicSO")]
public class RelicSO : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public BehaviourInfo behaviourInfo;
}
