using UnityEngine;

public class PlayerJumpState : IState
{
    private Player _player;
    private PlayerStateMachine _stateMachine;

    public PlayerJumpState(PlayerStateMachine playerStateMachine, Player player)
    {
        _player = player;
        _stateMachine = playerStateMachine;
    }

    public void Enter()
    {
        _player.Animation.PlayAnimation("Jump");

        _player.Movement.HandleJump();   
    }

    public void Execute()
    {
        if (!_player.Hit.IsStunned())
        {
            _player.Movement.HandleMovement();
        }

        if (Time.time - _player.Movement.JumpStartTime < _player.Movement.MinJumpDuration)
        {
            return;
        }

        if (_player.Movement.IsGrounded && _player.Input.Horizontal == 0)
        {
            _stateMachine.TransitionTo(_stateMachine.IdleState); // Transition to idle state
        }

        if (_player.Movement.IsGrounded && _player.Input.Horizontal != 0)
        {
            _stateMachine.TransitionTo(_stateMachine.RunState); // Transition to run state
        }

        if (!_player.Movement.IsGrounded && _player.Rigidbody.linearVelocity.y < 0)
        {
            _stateMachine.TransitionTo(_stateMachine.FallState); // Transition to fall state
            return;
        }

        if (_player.Hit.IsStunned())
        {
            _stateMachine.TransitionTo(_stateMachine.HitState); // Transition to take damage state
        }
    }

    public void Exit()
    {

    }
}
