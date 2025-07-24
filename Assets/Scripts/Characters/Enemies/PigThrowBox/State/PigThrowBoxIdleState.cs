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
        _pigThrowBox.Movement.ResetIdleTimer();
        _pigThrowBox.Rigidbody.linearVelocity = Vector2.zero;
    }

    public void Execute()
    {
        _pigThrowBox.Movement.UpdateIdleTimer();

        if (_pigThrowBox.Movement.IdleTimer >= _pigThrowBox.Movement.IdleDuration)
        {
            _pigThrowBox.Movement.FlipDirection();
            _stateMachine.TransitionTo(_pigThrowBox.WalkState); // Transition to WalkState after idle duration
        }

        if (!_pigThrowBox.Attack.IsAttacking && _pigThrowBox.GetComponent<IPigThrowBoxAttack>().HitPlayer)
        {
            _stateMachine.TransitionTo(_pigThrowBox.AttackState); // Transition to AttackState if player is hit
        }
    }

    public void Exit()
    {

    }
}
