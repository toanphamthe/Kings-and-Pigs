using UnityEngine;

public class PigWithMatchAttackState : IState
{
    private PigWithMatch _pigWithMatch;
    private EnemyStateMachine _stateMachine;

    public PigWithMatchAttackState(PigWithMatch pigWithMatch, EnemyStateMachine stateMachine)
    {
        _pigWithMatch = pigWithMatch;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pigWithMatch.Animation.PlayAnimation("LightingMatch");
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        _pigWithMatch.Attack.PerformAttack();
        _pigWithMatch.Cannon.Fire();
    }
}
