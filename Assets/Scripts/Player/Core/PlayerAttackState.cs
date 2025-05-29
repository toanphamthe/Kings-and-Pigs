using UnityEngine;

public class PlayerAttackState : IState
{
    private IPlayerAnimation _playerAnimation;

    private IAttackable _playerAttack;
    private Rigidbody2D _rb;
    private IDamageable _playerTakeDamage;

    private PlayerStateMachine _stateMachine;

    public PlayerAttackState(PlayerStateMachine playerStateMachine, Player player)
    {
        _stateMachine = playerStateMachine;
        _playerAnimation = player.PlayerAnimation;
        _playerAttack = player.PlayerAttack;
        _rb = player.GetComponent<Rigidbody2D>();
        _playerTakeDamage = player.PlayerTakeDamage;
    }

    public void Enter()
    {
        _playerAnimation.ChangeAnimation("Attack");
    }

    public void Execute()
    {
        _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y); // Stop horizontal movement during attack

        if (!_playerAttack.IsAttacking)
        {
            _stateMachine.TransitionTo(_stateMachine.IdleState); // Transition to idle state
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
