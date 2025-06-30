using UnityEngine;

public class PlayerHitState : IState
{
    private Player _player;
    private PlayerStateMachine _stateMachine;

    public PlayerHitState(PlayerStateMachine playerStateMachine, Player player)
    {
        _player = player;
        _stateMachine = playerStateMachine;
    }

    public void Enter()
    {
        _player.Animation.PlayAnimation("Hit");
    }

    public void Execute()
    {
        if (!_player.Hit.IsStunned())
        {
            _stateMachine.TransitionTo(_stateMachine.IdleState); // Transition to idle state
        }
        if (!_player.Hit.IsStunned() && _player.Health.CurrentHealth == 0)
        {
            _stateMachine.TransitionTo(_stateMachine.DeathState); // Transition to death state if health is 0
        }
    }

    public void Exit()
    {

    }
}
