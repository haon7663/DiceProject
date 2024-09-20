using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceCountPanel : MonoBehaviour
{
    public int Count { get; private set; }
    [SerializeField] private TMP_Text countLabel;

    public void SetLabel(int count)
    {
        Count = count;
        countLabel.text = count.ToString();
    }
}
