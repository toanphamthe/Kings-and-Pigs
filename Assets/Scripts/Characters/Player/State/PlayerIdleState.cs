using UnityEngine;

public class PlayerIdleState : IState
{
    private Player _player;
    private PlayerStateMachine _stateMachine;

    public PlayerIdleState(PlayerStateMachine playerStateMachine, Player player)
    {
        _player = player;
        _stateMachine = playerStateMachine;
    }

    public void Enter()
    {
        _player.Animation.PlayAnimation("Idle");
    }

    public void Execute()
    {
        if (_player.Input.Horizontal != 0 && _player.Movement.IsGrounded)
        {
            _stateMachine.TransitionTo(_stateMachine.RunState); // Transition to run state
        }   

        if (_player.Input.IsJumping && _player.Movement.IsGrounded)
        {
            _stateMachine.TransitionTo(_stateMachine.JumpState); // Transition to jump state
        }

        if (_player.Rigidbody.linearVelocity.y < 0 && !_player.Movement.IsGrounded)
        {
            _stateMachine.TransitionTo(_stateMachine.FallState); // Transition to fall state
        }

        if (_player.Input.IsAttacking && _player.Movement.IsGrounded && !_player.Attack.IsAttacking)
        {
            _stateMachine.TransitionTo(_stateMachine.AttackState); // Transition to attack state
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
