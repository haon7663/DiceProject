using System.Collections;

public class PrepareBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(PrepareBattle());
    }

    private IEnumerator PrepareBattle()
    {
        if (owner.enemy.TryGetComponent<Act>(out var enemyAct))
            enemyAct.Init();

        yield return YieldInstructionCache.WaitForSeconds(1f);

        ExecuteStartRelics();
        
        owner.ChangeState<TurnChangeState>();
    }

    private void ExecuteStartRelics()
    {
        if (owner.player.TryGetComponent<Relic>(out var relic))
        {
            var behaviourInfos = relic.GetInfosAndExecute(RelicActiveType.StartGame);
            
            var behaviours = behaviourInfos.CreateBehaviours();

            foreach (var behaviour in behaviours)
            {
                var target = behaviour.onSelf ? BattleController.Inst.player : BattleController.Inst.enemy;
            
                if (behaviour.GetType() == BehaviourType.Attack.GetBehaviourClass())
                {
                    if (target.TryGetComponent<Health>(out var health))
                    {
                        health.OnDamage(behaviour.value);
                    }
                }
            
                if (behaviour.GetType() == BehaviourType.StatusEffect.GetBehaviourClass())
                {
                    if (behaviour is StatusEffectBehaviour statusEffectBehaviour)
                    {
                        if (target.TryGetComponent<StatusEffect>(out var statusEffect))
                        {
                            statusEffect.AddEffect(statusEffectBehaviour.statusEffectSO, behaviour.value);
                        }
                    }
                }
            }
        }
    }
}