using UnityEngine;

public class PlayerRunState : IState
{
    private IPlayerAnimation _playerAnimation;
    private IPlayerInput _playerInput;
    private IPlayerMovement _playerMovement;
    private IAttackable _playerAttack;
    private IDamageable _playerTakeDamage;

    private PlayerStateMachine _stateMachine;

    public PlayerRunState(PlayerStateMachine playerStateMachine, Player player)
    {
        _stateMachine = playerStateMachine;
        _playerAnimation = player.PlayerAnimation;
        _playerInput = player.PlayerInput;
        _playerMovement = player.PlayerMovement;
        _playerAttack = player.PlayerAttack;
        _playerTakeDamage = player.PlayerTakeDamage;
    }

    public void Enter()
    {
        _playerAnimation.ChangeAnimation("Run");
    }

    public void Execute()
    {
        if (_playerInput.Horizontal == 0 && _playerMovement.IsGrounded)
        {
            _stateMachine.TransitionTo(_stateMachine.IdleState); // Transition to idle state
        }

        if (_playerInput.IsJumping && _playerMovement.IsGrounded)
        {
            _stateMachine.TransitionTo(_stateMachine.JumpState); // Transition to jump state
        }

        if (!_playerMovement.IsGrounded)
        {
            _stateMachine.TransitionTo(_stateMachine.FallState); // Transition to fall state
        }

        if (_playerInput.IsAttacking && _playerMovement.IsGrounded && !_playerAttack.IsAttacking)
        {
            _stateMachine.TransitionTo(_stateMachine.AttackState); // Transition to attack state
        }

        if (_playerTakeDamage.IsStunned())
        {
            _stateMachine.TransitionTo(_stateMachine.TakeDamageState); // Transition to death state
        }

        if (!_playerTakeDamage.IsStunned())
        {
            _playerMovement.HandleMovement();
        }
    }

    public void Exit()
    {

    }
}
