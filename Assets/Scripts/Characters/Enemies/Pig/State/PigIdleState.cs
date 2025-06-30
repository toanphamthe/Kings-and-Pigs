using UnityEngine;

public class PigIdleState : IState
{
    private Pig _pig;
    private EnemyStateMachine _stateMachine;

    public PigIdleState(Pig pig, EnemyStateMachine stateMachine)
    {
        _pig = pig;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pig.Animation.PlayAnimation("Idle");
        _pig.Movement.ResetIdleTimer();
        _pig.Rigidbody.linearVelocity = Vector2.zero;
    }

    public void Execute()
    {
        _pig.Movement.UpdateIdleTimer();

        if (_pig.Movement.IdleTimer >= _pig.Movement.IdleDuration)
        {
            _pig.Movement.FlipDirection();
            _stateMachine.TransitionTo(_pig.WalkState); // Transition to WalkState after idle duration
        }

        if (_pig.Hit.IsStunned())
        {
            _stateMachine.TransitionTo(_pig.HitState); // Transition to take damage state
        }
    }

    public void Exit()
    {

    }
}
