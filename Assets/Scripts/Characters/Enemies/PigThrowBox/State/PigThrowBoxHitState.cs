using UnityEngine;

public class PigThrowBoxHitState : IState
{
    private PigThrowBox _pigThrowBox;
    private EnemyStateMachine _stateMachine;

    public PigThrowBoxHitState(PigThrowBox pigThrowBox, EnemyStateMachine stateMachine)
    {
        _pigThrowBox = pigThrowBox;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pigThrowBox.Animation.PlayAnimation("Hit");
    }

    public void Execute()
    {
        if (!_pigThrowBox.Hit.IsStunned())
        {
            _stateMachine.TransitionTo(_pigThrowBox.IdleWithoutBoxState); // Transition to idle without box state
        }
        if (!_pigThrowBox.Hit.IsStunned() && _pigThrowBox.Hit.GetHealth() == 0)
        {
            _stateMachine.TransitionTo(_pigThrowBox.DeadState); // Transition to death state if health is 0
        }
    }

    public void Exit()
    {

    }
}

