using UnityEngine;

public class PlayerDeathState : IState
{
    private IPlayerAnimation _playerAnimation;
    private IPlayerInput _playerInput;
    private IPlayerMovement _playerMovement;

    private PlayerStateMachine _stateMachine;

    public PlayerDeathState(PlayerStateMachine playerStateMachine, Player player)
    {
        _stateMachine = playerStateMachine;
        _playerAnimation = player.PlayerAnimation;
        _playerInput = player.PlayerInput;
        _playerMovement = player.PlayerMovement;
    }

    public void Enter()
    {
        _playerAnimation.ChangeAnimation("Death");
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
