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

    [Header("- UI -")]
    public EventOptionController eventOptionController;
    public CardController cardController;
    public MapController mapController;
    public StatPanelController statPanelController;
    public DiceResultPanelController diceResultPanelController;
    public TurnOrderController turnOrderController;
    
    public PlayerData PlayerData { get; private set; }
    public Turn Turn { get; private set; }
    public Unit Unit { get; set; }

    private void Start()
    {
        PlayerData = dataManager.PlayerData;
        Turn = new Turn();
        
        ChangeState<InitBattleState>();
    }

    private IEnumerator StartTurn(Unit from, Unit to)
    {
        var isPlayerAtk = from.type == UnitType.Player;
        
        yield return YieldInstructionCache.WaitForSeconds(1f);
        
        var cards = enemy.unitSO.cards
            .Where(card => isPlayerAtk ? card.type == CardType.Defence : card.type == CardType.Attack).ToList();
        var card = cards[Random.Range(0, cards.Count)];

        yield return YieldInstructionCache.WaitForSeconds(1.5f);

        //주사위 굴려
        //var cardSO = CardController.Inst.playerPrepareCard.cardSO;
        //int diceValue = 0;
        //StartCoroutine(DiceManager.Inst.RollTheDices(cardSO.effects[0].diceTypes, 0, value => diceValue = value));
        //주사위 굴려
        
        yield return YieldInstructionCache.WaitForSeconds(1.5f);

        #region 카메라 연출 시작
        mainCameraMovement.VibrationForTime(0.5f);
        mainCameraMovement.ProductionAtTime(new Vector3(-0.4f, 0.35f, -10), 3, 4.4f);
        highlightCameraMovement.VibrationForTime(0.3f);
        highlightCameraMovement.ProductionAtTime(new Vector3(-0.4f, 0.35f, -10), 1, 4f);
        mainCameraVolumeSettings.SetVolume();
        #endregion
        
        if (from.TryGetComponent<Act>(out var fromAct) && to.TryGetComponent<Act>(out var toAct))
        {
            fromAct.AttackAction();
            toAct.HitAction();
        }
        yield return YieldInstructionCache.WaitForSeconds(1.2f);
        
        #region 카메라 연출 종료
        mainCameraMovement.ProductionAtTime(new Vector3(0, 0, -10), 0, 5, true);
        highlightCameraMovement.ProductionAtTime(new Vector3(0, 0, -10), 0, 5, true);
        mainCameraVolumeSettings.ResetVolume();
        #endregion
        
        yield return YieldInstructionCache.WaitForSeconds(1.2f);
        
        StartCoroutine(StartTurn(to, from));
    }
}
