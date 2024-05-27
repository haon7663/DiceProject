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

    public Image playerHealthBar;
    public TMP_Text playerHealthTMP;
    public TMP_Text playerDiceTMP;
    
    public Image enemyHealthBar;
    public TMP_Text enemyHealthTMP;
    public TMP_Text enemyDiceTMP;
    
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
