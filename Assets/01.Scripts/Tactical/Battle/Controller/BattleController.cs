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
    public DialogController dialogController;
    public TutorialPanelController tutorialPanelController;
    
    [Header("- Battle UI -")]
    public TurnOrderController turnOrderController;
    public DiceResultPanelController diceResultPanelController;
    
    [Header("- Event UI -")]
    public EventChoicesController eventChoicesController;
    
    [Header("- Store UI -")]
    public StorePanelController storePanelController;
    
    public PlayerData PlayerData { get; private set; }
    public Turn Turn { get; private set; }
    public Unit CurrentUnit { get; set; }
    public VictorType Victor { get; private set; }
    
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => DataManager.Inst.playerData != null);
        
        PlayerData = DataManager.Inst.playerData;
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
