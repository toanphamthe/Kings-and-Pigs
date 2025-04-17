using UnityEngine;

public class PlayerJumpState : IState
{
    private IPlayerAnimation _playerAnimation;
    private IPlayerInput _playerInput;
    private IPlayerMovement _playerMovement;

    private PlayerStateMachine _stateMachine;
    private Rigidbody2D _rb;

    public PlayerJumpState(PlayerStateMachine playerStateMachine, Player player)
    {
        _stateMachine = playerStateMachine;
        _playerAnimation = player.PlayerAnimation;
        _playerInput = player.PlayerInput;
        _playerMovement = player.PlayerMovement;
        _rb = player.GetComponent<Rigidbody2D>();
    }

    public void Enter()
    {
        _playerAnimation.ChangeAnimation("Jump");

        _playerMovement.HandleJump();
    }

    public void Execute()
    {
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
        }
    }

    public void Exit()
    {

    }
}
