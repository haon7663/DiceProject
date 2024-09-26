using UnityEngine;

[CreateAssetMenu(fileName = "AddCardAction", menuName = "GameActions/AddCardAction")]
public class AddCardAction : GameAction
{
    public CardSO cardSO;
    
    public override void Execute()
    {
        DataManager.Inst.playerData.Cards.Add(cardSO.ToJson());
    }

    public override string GetDialog()
    {
        return AddColor($"\"{cardSO.cardName}\" 카드 획득");
    }
}