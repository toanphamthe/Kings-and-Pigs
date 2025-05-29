using UnityEngine;

public class EnemyIdleState : IState
{
    private EnemyStateMachine _stateMachine;

    public EnemyIdleState(EnemyStateMachine enemyStateMachine, Enemy enemy)
    {
        _stateMachine = enemyStateMachine;
    }

    public void Enter()
    {

    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
