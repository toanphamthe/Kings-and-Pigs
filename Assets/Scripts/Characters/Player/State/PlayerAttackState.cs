using UnityEngine;

public class PlayerAttackState : IState
{
    private Player _player;

    private PlayerStateMachine _stateMachine;

    public PlayerAttackState(PlayerStateMachine playerStateMachine, Player player)
    {
        _player = player;
        _stateMachine = playerStateMachine;
    }

    public void Enter()
    {
        _player.Animation.PlayAnimation("Attack");
    }

    public void Execute()
    {
        _player.Rigidbody.linearVelocity = Vector2.zero; // Stop movement during attack

        if (!_player.Attack.IsAttacking)
        {
            _stateMachine.TransitionTo(_stateMachine.IdleState); // Transition to idle state
        }

        if (_player.Hit.IsStunned())
        {
            _player.Attack.StopAttack();
            _stateMachine.TransitionTo(_stateMachine.HitState); // Transition to take damage state
        }
    }

    public void Exit()
    {

    }
}
