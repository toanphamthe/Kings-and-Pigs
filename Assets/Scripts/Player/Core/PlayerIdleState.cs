using UnityEngine;

public class PlayerIdleState : IState
{
    private IPlayerAnimation _playerAnimation;
    private IPlayerInput _playerInput;
    private IPlayerMovement _playerMovement;
    private IPlayerAttack _playerAttack;

    private PlayerStateMachine _stateMachine;
    private Rigidbody2D _rb;

    public PlayerIdleState(PlayerStateMachine playerStateMachine, Player player)
    {
        _stateMachine = playerStateMachine;
        _playerAnimation = player.PlayerAnimation;
        _playerInput = player.PlayerInput;
        _playerMovement = player.PlayerMovement;
        _playerAttack = player.PlayerAttack;

        _rb = player.GetComponent<Rigidbody2D>();
    }

    public void Enter()
    {
        _playerAnimation.ChangeAnimation("Idle");
    }

    public void Execute()
    {
        if (_playerInput.Horizontal != 0 && _playerMovement.IsGrounded)
        {
            _stateMachine.TransitionTo(_stateMachine.RunState); // Transition to run state
        }

        if (_playerInput.IsJumping && _playerMovement.IsGrounded)
        {
            _stateMachine.TransitionTo(_stateMachine.JumpState); // Transition to jump state
        }

        if (_rb.linearVelocity.y < 0 && !_playerMovement.IsGrounded)
        {
            _stateMachine.TransitionTo(_stateMachine.FallState); // Transition to fall state
        }

        if (_playerInput.IsAttacking && _playerMovement.IsGrounded && !_playerAttack.IsAttacking)
        {
            _stateMachine.TransitionTo(_stateMachine.AttackState); // Transition to attack state
        }
    }

    public void Exit()
    {

    }
}
