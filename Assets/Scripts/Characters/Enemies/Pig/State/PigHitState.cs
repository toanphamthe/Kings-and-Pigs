using UnityEngine;

public class PigHitState : IState
{
    private Pig _pig;
    private EnemyStateMachine _stateMachine;

    public PigHitState(Pig pig, EnemyStateMachine stateMachine)
    {
        _pig = pig;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pig.Animation.PlayAnimation("Hit");
    }

    public void Execute()
    {
        if (!_pig.Hit.IsStunned())
        {
            _stateMachine.TransitionTo(_pig.IdleState); // Transition to idle state
        }
        if (!_pig.Hit.IsStunned() && _pig.Hit.GetHealth() == 0)
        {
            _stateMachine.TransitionTo(_pig.DeadState); // Transition to death state if health is 0
        }
    }

    public void Exit()
    {

    }
}
