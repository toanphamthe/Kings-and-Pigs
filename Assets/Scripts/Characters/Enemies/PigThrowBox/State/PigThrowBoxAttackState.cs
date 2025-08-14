using UnityEngine;

public class PigThrowBoxAttackState : IState
{
    private PigThrowBox _pigThrowBox;
    private EnemyStateMachine _stateMachine;

    public PigThrowBoxAttackState(PigThrowBox pigThrowBox, EnemyStateMachine stateMachine)
    {
        _pigThrowBox = pigThrowBox;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pigThrowBox.Animation.PlayAnimation("ThrowingBox");
        _pigThrowBox.Attack.Attack();
    }

    public void Execute()
    {
        if (_pigThrowBox.Hit.IsStunned())
        {
            _pigThrowBox.Attack.ResetAttack();
            _stateMachine.TransitionTo(_pigThrowBox.HitState); // Transition to hit state
        }
    }

    public void Exit()
    {

    }
}
