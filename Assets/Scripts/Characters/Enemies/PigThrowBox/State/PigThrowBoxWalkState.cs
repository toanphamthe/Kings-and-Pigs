using UnityEngine;

public class PigThrowBoxWalkState : IState
{
    private PigThrowBox _pigThrowBox;
    private EnemyStateMachine _stateMachine;

    public PigThrowBoxWalkState(PigThrowBox pigThrowBox, EnemyStateMachine stateMachine)
    {
        _pigThrowBox = pigThrowBox;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pigThrowBox.Animation.PlayAnimation("Run");
    }

    public void Execute()
    {
        if (!_pigThrowBox.Movement.IsGrounded || _pigThrowBox.Movement.IsHittingWall)
        {
            _stateMachine.TransitionTo(_pigThrowBox.IdleState); // Transition to IdleState if not grounded
        }

        if (!_pigThrowBox.Attack.IsAttacking && _pigThrowBox.GetComponent<PigThrowBoxAttack>().HitPlayer)
        {
            _stateMachine.TransitionTo(_pigThrowBox.AttackState); // Transition to AttackState if player is hit
        }

        _pigThrowBox.Movement.HandleMovement();
    }

    public void Exit()
    {

    }
}
