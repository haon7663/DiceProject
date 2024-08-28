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
    public CardController cardController;
    public MapController mapController;
    
    [Header("- Battle UI -")]
    public TurnOrderController turnOrderController;
    public DiceResultPanelController diceResultPanelController;
    
    [Header("- Event UI -")]
    public EventOptionController eventOptionController;
    
    public PlayerData PlayerData { get; private set; }
    public Turn Turn { get; private set; }
    public Unit CurrentUnit { get; set; }
    
    private void Start()
    {
        PlayerData = dataManager.PlayerData;
        Turn = new Turn();
        
        ChangeState<InitBattleState>();
    }
}
