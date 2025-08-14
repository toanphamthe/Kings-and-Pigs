using UnityEngine;

public class PigThrowBoxIdleWithoutBoxState : IState
{
    private PigThrowBox _pigThrowBox;
    private EnemyStateMachine _stateMachine;

    public PigThrowBoxIdleWithoutBoxState(PigThrowBox pigThrowBox, EnemyStateMachine stateMachine)
    {
        _pigThrowBox = pigThrowBox;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pigThrowBox.Animation.PlayAnimation("IdleWithoutBox");
    }

    public void Execute()
    {
        _pigThrowBox.Attack.UpdateAttackTimer();

        if (_pigThrowBox.Hit.IsStunned())
        {
            _stateMachine.TransitionTo(_pigThrowBox.HitState); // Transition to hit state
        }

        if (_pigThrowBox.Attack.AttackTimer <= _pigThrowBox.Attack.AttackCooldown) return;

        if (!_pigThrowBox.Attack.IsAttacking)
        {
            _pigThrowBox.Animation.PlayAnimation("PickingBox");
        }
    }

    public void Exit()
    {
        
    }
}
