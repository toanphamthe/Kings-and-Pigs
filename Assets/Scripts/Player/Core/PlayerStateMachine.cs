using UnityEngine;

public class PlayerStateMachine
{
    public IState CurrentState { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        IdleState = new PlayerIdleState(this, player);
        RunState = new PlayerRunState(this, player);
        JumpState = new PlayerJumpState(this, player);
        FallState = new PlayerFallState(this, player);
        DeathState = new PlayerDeathState(this, player);
        AttackState = new PlayerAttackState(this, player);
    }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    public void Update()
    {
        CurrentState?.Execute();
    }
}
