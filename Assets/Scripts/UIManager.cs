using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Transform canvas;
    
    [Header("턴")]
    [SerializeField] private Animator turnStatePanelAnimator;
    [SerializeField] private TMP_Text turnStateTMP;
    
    [Header("카드")]
    [SerializeField] private GameObject attackCardPanel;
    [SerializeField] private GameObject defenceCardPanel;
    
    [Header("주사위")]
    [SerializeField] private GameObject dicePanel;
    [SerializeField] private TMP_Text[] diceCountText;

    [Header("상태")]
    [SerializeField] private Image playerTopHealthBar;
    [SerializeField] private TMP_Text playerTopHealthTMP;
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private TMP_Text playerHealthTMP;
    [SerializeField] private TMP_Text playerDiceTMP;
    [Space]
    [SerializeField] private Image enemyHealthBar;
    [SerializeField] private TMP_Text enemyHealthTMP;
    [SerializeField] private TMP_Text enemyDiceTMP;
    [SerializeField] private TMP_Text enemyExpectDiceTMP;
    
    [Header("")] 
    [SerializeField] private DamageTextHandler damageTextHandlerPrefab;
    [SerializeField] private RecoveryTextHandler recoveryTextHandlerPrefab;
    [SerializeField] private GameObject avoidTextHandlerPrefab;

    [Header("상태이상")] 
    [SerializeField] private StatusEffectTextHandler statusEffectTextHandlerPrefab;

    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
    }

    public void PopDamageText(Vector3 pos, int value)
    {
        var damageText = Instantiate(damageTextHandlerPrefab, _camera.WorldToScreenPoint(pos), Quaternion.identity, canvas);
        damageText.Setup(value);
    }
    public void PopRecoveryText(Vector3 pos, int value)
    {
        var recoveryText = Instantiate(recoveryTextHandlerPrefab, _camera.WorldToScreenPoint(pos), Quaternion.identity, canvas);
        recoveryText.Setup(value);
    }
    
    public void PopAvoidText(Vector3 pos)
    {
        Instantiate(avoidTextHandlerPrefab, _camera.WorldToScreenPoint(pos), Quaternion.identity, canvas);
    }

    public void PopStatusEffectText(Vector2 pos, StatusEffectSO statusEffectSO, int stack)
    {
        var text = Instantiate(statusEffectTextHandlerPrefab, _camera.WorldToScreenPoint(pos), Quaternion.identity, canvas);
        text.SetUp(statusEffectSO, stack);
    }

    public void ShowTurnState(bool isPlayerTurn)
    {
        turnStatePanelAnimator.gameObject.SetActive(true);
        turnStatePanelAnimator.SetTrigger("show");
        turnStateTMP.text = isPlayerTurn ? "플레이어 행동" : "적 행동";
    }
    
    public void ChangeCardPanel(bool isPlayerTurn)
    {
        attackCardPanel.SetActive(isPlayerTurn);
        defenceCardPanel.SetActive(!isPlayerTurn);
    }
    public void CloseCardPanels()
    {
        attackCardPanel.SetActive(false);
        defenceCardPanel.SetActive(false);
    }
    public void OpenDicePanel()
    {
        dicePanel.SetActive(true);
    }
    public void CloseDicePanel()
    {
        dicePanel.SetActive(false);
    }

    public void SetDiceCountText()
    {
        for (var i = 1; i < 5; i++)
        {
            diceCountText[i].text = DataManager.Inst.diceCount[(DiceType)i].ToString();
        }
    }

    public void SetHealth(float curHp, float maxHp, bool isPlayer)
    {
        if (isPlayer)
        {
            playerTopHealthBar.fillAmount = curHp / maxHp;
            playerTopHealthTMP.text = curHp + " / " + maxHp;
            playerHealthBar.fillAmount = curHp / maxHp;
            playerHealthTMP.text = curHp + " / " + maxHp;
        }
        else
        {
            enemyHealthBar.fillAmount = curHp / maxHp;
            enemyHealthTMP.text = curHp + " / " + maxHp;
        }
    }
    
    public void SetExpectValue(int minValue, int maxValue, bool isPlayer)
    {
        if (isPlayer)
            enemyExpectDiceTMP.text = minValue + "~" + maxValue;
        else
            enemyExpectDiceTMP.text = minValue + "~" + maxValue;
    }

    public void CloseExpectValueText()
    {
        enemyExpectDiceTMP.text = "";
    }

    public void SetValue(int value, bool isPlayer)
    {
        if (isPlayer)
            DOVirtual.Float(0, value, 0.2f, x => playerDiceTMP.text = ((int)x).ToString());
        else
            DOVirtual.Float(0, value, 0.2f, x => enemyDiceTMP.text = ((int)x).ToString());
    }

    private void CloseValueText()
    {
        playerDiceTMP.text = "";
        enemyDiceTMP.text = "";
    }

    private void OnEnable()
    {
        if (TurnManager.inst == null)
        {
            Debug.LogError("TurnManager 인스턴스가 초기화되지 않았습니다.");
            return;
        }
        
        TurnManager.inst.OnCreatureTurnStart += ShowTurnState;
        TurnManager.inst.OnCreatureTurnStart += ChangeCardPanel;
        TurnManager.inst.OnActionComplete += CloseDicePanel;
        TurnManager.inst.OnActionComplete += CloseValueText;
    }
    private void OnDisable()
    {
        if (TurnManager.inst == null)
        {
            Debug.LogError("TurnManager 인스턴스가 초기화되지 않았습니다.");
            return;
        }
        
        TurnManager.inst.OnCreatureTurnStart -= ShowTurnState;
        TurnManager.inst.OnCreatureTurnStart -= ChangeCardPanel;
        TurnManager.inst.OnActionComplete -= CloseDicePanel;
        TurnManager.inst.OnActionComplete -= CloseValueText;
    }
}
