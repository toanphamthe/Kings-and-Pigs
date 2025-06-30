using UnityEngine;

public class PigDeadState : IState
{
    private Pig _pig;
    private EnemyStateMachine _stateMachine;

    public PigDeadState(Pig pig, EnemyStateMachine stateMachine)
    {
        _pig = pig;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pig.Animation.PlayAnimation("Dead");
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
