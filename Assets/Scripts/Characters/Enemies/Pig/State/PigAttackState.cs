using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PigAttackState : IState
{
    private Pig _pig;
    private EnemyStateMachine _stateMachine;

    public PigAttackState(Pig pig, EnemyStateMachine stateMachine)
    {
        _pig = pig;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pig.Animation.PlayAnimation("Attack");
        _pig.Attack.Attack();
    }

    public void Execute()
    {
        _pig.Attack.UpdateAttackTimer();
        if (_pig.Hit.IsStunned())
        {
            _stateMachine.TransitionTo(_pig.HitState); // Transition to hit state
        }
        else
        {
            _pig.Rigidbody.linearVelocity = Vector2.zero;
        }
        if (_pig.Attack.AttackTimer <= _pig.Attack.AttackCooldown) return;

        if (!_pig.Attack.IsAttacking && !_pig.GetComponent<PigAttack>().HitPlayer)
        {
            _stateMachine.TransitionTo(_pig.WalkState); //Transition to WalkState if not attacking and no player hit
        }
        else if (!_pig.Attack.IsAttacking && _pig.GetComponent<PigAttack>().HitPlayer)
        {
            _stateMachine.TransitionTo(_pig.AttackState); // Stay in AttackState if player is hit
        }
    }

    public void Exit()
    {
        _pig.Attack.ResetAttack(); // Reset the attack state when exiting
    }
}
