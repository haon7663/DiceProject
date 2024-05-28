using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;

    private void Awake()
    {
        inst = this;
    }
    
    [Header("카드")]
    [SerializeField] private GameObject attackCardPanel;
    [SerializeField] private GameObject defenceCardPanel;
    
    [Header("주사위")]
    [SerializeField] private GameObject dicePanel;
    [SerializeField] private TMP_Text diceTotalTMP;

    [Header("상태")]
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private TMP_Text playerHealthTMP;
    [SerializeField] private TMP_Text playerDiceTMP;
    [Space]
    [SerializeField] private Image enemyHealthBar;
    [SerializeField] private TMP_Text enemyHealthTMP;
    [SerializeField] private TMP_Text enemyDiceTMP;


    public void ChangeCardPanel(bool attack)
    {
        attackCardPanel.SetActive(attack);
        defenceCardPanel.SetActive(!attack);
    }
    public void OpenDicePanel()
    {
        dicePanel.SetActive(true);
    }
    public void SetDiceTotalTMP(int total)
    {
        diceTotalTMP.text = total.ToString();
    }

    public void SetHealth(float curHp, float maxHp, bool isPlayer)
    {
        if (isPlayer)
        {
            playerHealthBar.fillAmount = curHp / maxHp;
            playerHealthTMP.text = curHp + " / " + maxHp;
        }
        else
        {
            enemyHealthBar.fillAmount = curHp / maxHp;
            enemyHealthTMP.text = curHp + " / " + maxHp;
        }
    }

    public void SetValue(int value, bool isPlayer)
    {
        if (isPlayer)
            playerDiceTMP.text = value.ToString();
        else
            enemyDiceTMP.text = value.ToString();
    }
}
