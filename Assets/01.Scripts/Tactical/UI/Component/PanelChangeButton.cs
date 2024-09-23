using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelChangeButton : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text label;

    [SerializeField] private Sprite selectSprite;
    [SerializeField] private Sprite defaultSprite;
    
    public void Active()
    {
        background.color = Color.white;
        icon.color = Color.white;
        label.color = Color.white;
        background.sprite = selectSprite;
    }

    public void DeActive()
    {
        background.color = Color.gray;
        icon.color = Color.gray;
        label.color = Color.gray;
        background.sprite = defaultSprite;
    }
}
