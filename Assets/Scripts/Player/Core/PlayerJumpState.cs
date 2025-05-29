using UnityEngine;

public class PlayerJumpState : IState
{
    private IPlayerAnimation _playerAnimation;
    private IPlayerInput _playerInput;
    private IPlayerMovement _playerMovement;
    private IDamageable _playerTakeDamage;

    private PlayerStateMachine _stateMachine;
    private Rigidbody2D _rb;

    private float _jumpStartTime;
    private float _minJumpDuration = 0.2f;

    public PlayerJumpState(PlayerStateMachine playerStateMachine, Player player)
    {
        _stateMachine = playerStateMachine;
        _playerAnimation = player.PlayerAnimation;
        _playerInput = player.PlayerInput;
        _playerMovement = player.PlayerMovement;
        _rb = player.GetComponent<Rigidbody2D>();
        _playerTakeDamage = player.PlayerTakeDamage;
    }

    public void Enter()
    {
        _playerAnimation.ChangeAnimation("Jump");

        _playerMovement.HandleJump();   

        _jumpStartTime = Time.time;
    }

    public void Execute()
    {
        if (!_playerTakeDamage.IsStunned())
        {
            _playerMovement.HandleMovement();
        }

        if (Time.time - _jumpStartTime < _minJumpDuration)
        {
            return;
        }

        if (_playerMovement.IsGrounded && _playerInput.Horizontal == 0)
        {
            _stateMachine.TransitionTo(_stateMachine.IdleState); // Transition to idle state
        }

        if (_playerMovement.IsGrounded && _playerInput.Horizontal != 0)
        {
            _stateMachine.TransitionTo(_stateMachine.RunState); // Transition to run state
        }

        if (!_playerMovement.IsGrounded && _rb.linearVelocity.y < 0)
        {
            _stateMachine.TransitionTo(_stateMachine.FallState); // Transition to fall state
            return;
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
