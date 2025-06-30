using UnityEngine;

public class PlayerRunState : IState
{
    private Player _player;
    private PlayerStateMachine _stateMachine;

    public PlayerRunState(PlayerStateMachine playerStateMachine, Player player)
    {
        _player = player;
        _stateMachine = playerStateMachine;
    }

    public void Enter()
    {
        _player.Animation.PlayAnimation("Run");
    }

    public void Execute()
    {
        if (_player.Input.Horizontal == 0 && _player.Movement.IsGrounded)
        {
            _stateMachine.TransitionTo(_stateMachine.IdleState); // Transition to idle state
        }

        if (_player.Input.IsJumping && _player.Movement.IsGrounded)
        {
            _stateMachine.TransitionTo(_stateMachine.JumpState); // Transition to jump state
        }

        if (!_player.Movement.IsGrounded)
        {
            _stateMachine.TransitionTo(_stateMachine.FallState); // Transition to fall state
        }

        if (_player.Input.IsAttacking && _player.Movement.IsGrounded && !_player.Attack.IsAttacking)
        {
            _stateMachine.TransitionTo(_stateMachine.AttackState); // Transition to attack state
        }

        if (_player.Hit.IsStunned())
        {
            _stateMachine.TransitionTo(_stateMachine.HitState); // Transition to death state
        }

        if (!_player.Hit.IsStunned())
        {
            _player.Movement.HandleMovement();
        }
    }

    public void Exit()
    {

    }
}
