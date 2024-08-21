using System.Collections;
using System.Linq;
using Map;
using UnityEngine;
using UnityEngine.Serialization;

public class BattleController : Singleton<BattleController>
{
    public Unit playerUnit;
    public Unit enemyUnit;

    public PlayerData PlayerData { get; private set; }

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

    private void Start()
    {
        PlayerData = dataManager.PlayerData;
        
        SetGame();
        StartCoroutine(StartTurn(playerUnit, enemyUnit));
    }

    private void SetGame()
    {
        playerUnit.GetComponent<Health>().maxHp = playerUnit.GetComponent<Health>().curHp = PlayerData.curHp;
        statPanelController.ConnectPanel(playerUnit);
        
        enemyUnit.GetComponent<Health>().maxHp = enemyUnit.GetComponent<Health>().curHp = enemyUnit.unitSO.maxHp;
        statPanelController.ConnectPanel(enemyUnit);
        
        cardController.InitDeck(PlayerData.cards.ToCard());
    }

    private IEnumerator StartTurn(Unit from, Unit to)
    {
        var isPlayerAtk = from.type == UnitType.Player;
        
        turnOrderController.ShowPanel(isPlayerAtk);
        
        yield return YieldInstructionCache.WaitForSeconds(1f);
        
        var cards = enemyUnit.unitSO.cards
            .Where(card => isPlayerAtk ? card.type == CardType.Defence : card.type == CardType.Attack).ToList();
        var card = cards[Random.Range(0, cards.Count)];
        cardController.CopyToPrepareCard(card);

        yield return YieldInstructionCache.WaitForSeconds(1.5f);

        yield return new WaitUntil(() => CardController.Inst.playerPrepareCard);

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
