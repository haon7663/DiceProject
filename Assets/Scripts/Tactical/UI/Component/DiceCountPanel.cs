using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceCountPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text countLabel;

    public void SetLabel(int count)
    {
        countLabel.text = count.ToString();
    }
}
