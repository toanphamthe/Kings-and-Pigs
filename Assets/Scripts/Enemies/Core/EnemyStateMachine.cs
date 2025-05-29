using UnityEngine;

public class EnemyStateMachine
{
    public IState CurrentState { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyPatrolState PatrolState { get; private set; }

    public EnemyStateMachine(Enemy enemy)
    {
        IdleState = new EnemyIdleState(this, enemy);
        PatrolState = new EnemyPatrolState(this, enemy);
        //ChaseState = new EnemyChaseState(this, enemy);
        //AttackState = new EnemyAttackState(this, enemy);
        //DeathState = new EnemyDeathState(this, enemy);
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
