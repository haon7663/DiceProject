using UnityEngine;

public class ItemReward : Reward
{
    private ItemSO _itemSO;

    public ItemReward(ItemSO itemSO)
    {
        _itemSO = itemSO;
    }
    
    public override Sprite GetSprite()
    {
        return _itemSO.sprite;
    }

    public override string GetLabel()
    {
        return _itemSO.itemName;
    }

    public override void Execute()
    {
        for (var i = 0; i < 8; i++)
        {
            if (DataManager.Inst.playerData.items[i] != "") continue;
            DataManager.Inst.playerData.items[i] = _itemSO.ToJson();
            break;
        }
    }
}