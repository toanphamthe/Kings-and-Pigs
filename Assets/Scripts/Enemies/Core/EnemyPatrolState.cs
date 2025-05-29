using UnityEngine;

public class EnemyPatrolState : IState
{
    private EnemyStateMachine _stateMachine;
    public EnemyPatrolState(EnemyStateMachine enemyStateMachine, Enemy enemy)
    {
        _stateMachine = enemyStateMachine;
    }

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
