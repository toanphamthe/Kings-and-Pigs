using UnityEngine;

public class PlayerTakeDamageState : IState
{
    private IPlayerAnimation _playerAnimation;
    private IPlayerInput _playerInput;
    private IPlayerMovement _playerMovement;
    private IAttackable _playerAttack;
    private IDamageable _playerTakeDamage;

    private PlayerStateMachine _stateMachine;

    public PlayerTakeDamageState(PlayerStateMachine playerStateMachine, Player player)
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
        _playerAnimation.ChangeAnimation("TakeDamage");
    }

    public void Execute()
    {
        if (!_playerTakeDamage.IsStunned())
        {
            _stateMachine.TransitionTo(_stateMachine.IdleState); // Transition to idle state
        }
    }

    public void Exit()
    {

    }
}
