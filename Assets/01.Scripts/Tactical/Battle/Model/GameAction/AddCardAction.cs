using UnityEngine;

[CreateAssetMenu(fileName = "AddCardAction", menuName = "GameActions/AddCardAction")]
public class AddCardAction : GameAction
{
    public CardSO cardSO;
    
    public override void Execute()
    {
        DataManager.Inst.playerData.Cards.Add(cardSO.ToJson());
    }
}