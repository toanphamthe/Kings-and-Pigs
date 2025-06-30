using UnityEngine;

public class PigWalkState : IState
{
    private Pig _pig;
    private EnemyStateMachine _stateMachine;

    public PigWalkState(Pig pig, EnemyStateMachine stateMachine)
    {
        _pig = pig;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pig.Animation.PlayAnimation("Run");
    }

    public void Execute()
    {
        if (!_pig.Movement.IsGrounded)
        {
            _stateMachine.TransitionTo(_pig.IdleState); // Transition to IdleState if not grounded
        }

        if (!_pig.Attack.IsAttacking && _pig.GetComponent<PigAttack>().HitPlayer)
        {
            _stateMachine.TransitionTo(_pig.AttackState); // Transition to AttackState if player is hit
        }

        if (_pig.Hit.IsStunned())
        {
            _stateMachine.TransitionTo(_pig.HitState); // Transition to take damage state
        }

        if (!_pig.Hit.IsStunned())
        {
            _pig.Movement.HandleMovement();
        }
    }

    public void Exit()
    {

    }
}
