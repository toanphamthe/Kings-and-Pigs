using UnityEngine;

public class PigWithMatchIdleState : IState
{
    private PigWithMatch _pigWithMatch;
    private EnemyStateMachine _stateMachine;

    public PigWithMatchIdleState(PigWithMatch pigWithMatch, EnemyStateMachine stateMachine)
    {
        _pigWithMatch = pigWithMatch;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pigWithMatch.Animation.PlayAnimation("Idle");
    }

    public void Execute()
    {
        _pigWithMatch.Attack.UpdateAttackTimer();

        if (_pigWithMatch.Attack.AttackTimer <= _pigWithMatch.Attack.AttackCooldown) return;

        _stateMachine.TransitionTo(_pigWithMatch.AttackState); // Transition to attack state
    }

    public void Exit()
    {
        _pigWithMatch.Attack.ResetAttackTimer();
    }
}
