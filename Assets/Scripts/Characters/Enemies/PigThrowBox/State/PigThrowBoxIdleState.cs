using UnityEngine;

public class PigThrowBoxIdleState : IState
{
    private PigThrowBox _pigThrowBox;
    private EnemyStateMachine _stateMachine;

    public PigThrowBoxIdleState(PigThrowBox pigThrowBox, EnemyStateMachine stateMachine)
    {
        _pigThrowBox = pigThrowBox;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pigThrowBox.Animation.PlayAnimation("Idle");
        _pigThrowBox.Rigidbody.linearVelocity = Vector2.zero;
    }

    public void Execute()
    {
        if (!_pigThrowBox.Attack.IsAttacking)
        {
            _stateMachine.TransitionTo(_pigThrowBox.AttackState); // Transition to AttackState if player is hit
        }

        if (_pigThrowBox.Hit.IsStunned())
        {
            _stateMachine.TransitionTo(_pigThrowBox.HitState); // Transition to hit state
        }
    }

    public void Exit()
    {

    }
}
