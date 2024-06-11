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

    [SerializeField] private Transform canvas;
    
    [Header("턴")]
    [SerializeField] private Animator turnStatePanelAnimator;
    [SerializeField] private TMP_Text turnStateTMP;
    
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
    
    [Header("")] 
    [SerializeField] private DamageTextHandler damageTextHandlerPrefab;
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

    private void CloseValueText()
    {
        print("turnEnd");
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
