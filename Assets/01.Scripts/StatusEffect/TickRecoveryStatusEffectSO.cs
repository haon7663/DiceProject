using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TickRecoveryStatusEffectSO", menuName = "Scriptable Object/StatusEffectSO/TickRecoveryStatusEffectSO")]
public class TickRecoveryStatusEffectSO : StatusEffectSO
{
    [Header("틱 회복량")]
    [SerializeField] private bool useStack;
    [DrawIf("useStack", false)]
    [SerializeField] private int value;
    [DrawIf("useStack", true)]
    [SerializeField] private int multiplierValue;
    
    public override void UpdateEffect(Unit unit)
    {
        if (unit.TryGetComponent<Health>(out var health))
        {
            health.OnRecovery(GetCurrentValue());
            var log = $"{label}으로(로) {unit.unitSO.name}의 체력이 {GetCurrentValue()} 회복되었다!";
            BattleController.Inst.dialogController.GenerateDialog(log.ConvertKoreaStringJongSung());
        }
        base.UpdateEffect(unit);
    }
    
    public override string GetDialogString(Unit unit)
    {
        return $"{unit.unitSO.name}은(는) {name}으로(로) {GetCurrentValue()} 회복했다!";
    }

    
    public override int GetCurrentValue()
    {
        return useStack ? GetCurrentStack() * multiplierValue : value;
    }
}
