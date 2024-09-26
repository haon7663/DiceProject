using UnityEngine;

public class RelicReward : Reward
{
    public RelicSO relicSO;
    
    public RelicReward(RelicSO relicSO)
    {
        this.relicSO = relicSO;
    }
    
    public override Sprite GetSprite()
    {
        return relicSO.sprite;
    }

    public override string GetLabel()
    {
        return relicSO.name;
    }
    
    public override Vector2 GetSizeDelta()
    {
        return Vector2.one * 125f;
    }


    public override void Execute()
    {
        DataManager.Inst.playerData.Relics.Add(relicSO.ToJson());
    }
}