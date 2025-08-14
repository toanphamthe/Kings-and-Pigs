using UnityEngine;

public class PlayerDeathState : IState
{
    private Player _player;
    private PlayerStateMachine _stateMachine;

    public PlayerDeathState(PlayerStateMachine playerStateMachine, Player player)
    {
        _player = player;
        _stateMachine = playerStateMachine;
    }

    public void Enter()
    {
        _player.Animation.PlayAnimation("Dead");
        _player.PlaySFX("Death");
        _player.Input.DisableInput();
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
