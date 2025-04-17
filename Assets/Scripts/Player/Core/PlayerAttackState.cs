using UnityEngine;

public class PlayerAttackState : IState
{
    private IPlayerAnimation _playerAnimation;
    private IPlayerInput _playerInput;
    private IPlayerMovement _playerMovement;
    private IPlayerAttack _playerAttack;
    private Rigidbody2D _rb;

    private PlayerStateMachine _stateMachine;

    public PlayerAttackState(PlayerStateMachine playerStateMachine, Player player)
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
        _playerAnimation.ChangeAnimation("Attack");
        _playerAttack.HandleAttack();
    }

    public void Execute()
    {
        _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y); // Stop horizontal movement during attack

        if (!_playerAttack.IsAttacking)
        {
            _stateMachine.TransitionTo(_stateMachine.IdleState); // Transition to idle state
        }
    }

    public void Exit()
    {

    }
}
