using UnityEngine;

public class PlayerFallState : IState
{
    private Player _player;
    private PlayerStateMachine _stateMachine;

    public PlayerFallState(PlayerStateMachine playerStateMachine, Player player)
    {
        _player = player;
        _stateMachine = playerStateMachine;
    }

    public void Enter()
    {
        _player.Animation.PlayAnimation("Fall");
    }

    public void Execute()
    {
        if (!_player.Hit.IsStunned())
        {
            _player.Movement.HandleMovement();
        }

        if (_player.Movement.IsGrounded && _player.Input.Horizontal == 0)
        {
            _stateMachine.TransitionTo(_stateMachine.IdleState); // Transition to idle state
        }

        if (_player.Movement.IsGrounded && _player.Input.Horizontal != 0)
        {
            _stateMachine.TransitionTo(_stateMachine.RunState); // Transition to run state
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
