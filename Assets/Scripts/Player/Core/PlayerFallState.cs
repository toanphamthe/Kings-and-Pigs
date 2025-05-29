using UnityEngine;

public class PlayerFallState : IState
{
    private IPlayerAnimation _playerAnimation;
    private IPlayerInput _playerInput;
    private IPlayerMovement _playerMovement;
    private IDamageable _playerTakeDamage;

    private PlayerStateMachine _stateMachine;

    public PlayerFallState(PlayerStateMachine playerStateMachine, Player player)
    {
        _stateMachine = playerStateMachine;
        _playerAnimation = player.PlayerAnimation;
        _playerInput = player.PlayerInput;
        _playerMovement = player.PlayerMovement;
        _playerTakeDamage = player.PlayerTakeDamage;
    }

    public void Enter()
    {
        _playerAnimation.ChangeAnimation("Fall");
    }

    public void Execute()
    {
        if (!_playerTakeDamage.IsStunned())
        {
            _playerMovement.HandleMovement();
        }

        if (_playerMovement.IsGrounded && _playerInput.Horizontal == 0)
        {
            _stateMachine.TransitionTo(_stateMachine.IdleState); // Transition to idle state
        }

        if (_playerMovement.IsGrounded && _playerInput.Horizontal != 0)
        {
            _stateMachine.TransitionTo(_stateMachine.RunState); // Transition to run state
        }

        if (_playerTakeDamage.IsStunned())
        {
            _stateMachine.TransitionTo(_stateMachine.TakeDamageState); // Transition to take damage state
        }
    }

    public void Exit()
    {

    }
}
