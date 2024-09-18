using System;
using System.Collections;
using System.Linq;
using Map;
using UnityEngine;

public class BattleController : StateMachine
{
    public static BattleController Inst;

    private void Awake()
    {
        Inst = this;
    }

    
    [Header("- SO -")]
    public UnitSO enemyData;
    public EventData eventData;

    [Header("- Units -")]
    public Unit player;
    public Unit enemy;
    public EventObject eventObject;

    [Header("- Management -")]
    public DataManager dataManager;

    [Header("- Camera -")]
    public CameraMovement mainCameraMovement;
    public CameraMovement highlightCameraMovement;
    public VolumeSettings mainCameraVolumeSettings;
    
    [Header("- Public UI -")]
    public InteractionPanelController interactionPanelController;
    public TopPanelController topPanelController;
    public StatPanelController statPanelController;
    public InteractionCardController interactionCardController;
    public DisplayCardController displayCardController;
    public MapController mapController;
    public HudController hudController;
    public RewardPanelController rewardPanelController;
    public DiceCountPanelController diceCountPanelController;
    public GoldPanelController goldPanelController;
    
    [Header("- Battle UI -")]
    public TurnOrderController turnOrderController;
    public DiceResultPanelController diceResultPanelController;
    
    [Header("- Event UI -")]
    public EventChoicesController eventChoicesController;
    
    public PlayerData PlayerData { get; private set; }
    public Turn Turn { get; private set; }
    public Unit CurrentUnit { get; set; }
    public VictorType Victor { get; private set; }
    
    private void Start()
    {
        PlayerData = dataManager.playerData;
        Turn = new Turn();

        ChangeState<InitState>();
    }

    public void VictoryEventHandler()
    {
        Victor = VictorType.Player;
    }
    
    public void DefeatEventHandler()
    {
        Victor = VictorType.Enemy;
    }
}
