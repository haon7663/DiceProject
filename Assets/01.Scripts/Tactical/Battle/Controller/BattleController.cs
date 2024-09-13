using System.Collections;
using System.Linq;
using Map;
using UnityEngine;
using UnityEngine.Serialization;

public class BattleController : StateMachine
{
    [Header("- Units -")]
    public Unit player;
    public Unit enemy;

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
    
    [Header("- Battle UI -")]
    public TurnOrderController turnOrderController;
    public DiceResultPanelController diceResultPanelController;
    
    [Header("- Event UI -")]
    public EventOptionController eventOptionController;
    
    public PlayerData PlayerData { get; private set; }
    public Turn Turn { get; private set; }
    public Unit CurrentUnit { get; set; }
    public VictorType Victor { get; private set; }
    
    private void Start()
    {
        PlayerData = dataManager.playerData;
        Turn = new Turn();

        switch (GameManager.Inst.currentGameMode)
        {
            case GameMode.Battle:
                ChangeState<InitBattleState>();
                break;
            case GameMode.Event:
                ChangeState<InitEventState>();
                break;
        }
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
