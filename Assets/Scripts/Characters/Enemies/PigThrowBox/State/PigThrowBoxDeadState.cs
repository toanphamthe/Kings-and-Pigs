using UnityEngine;

public class PigThrowBoxDeadState : IState
{
    private PigThrowBox _pigThrowBox;
    private EnemyStateMachine _stateMachine;

    public PigThrowBoxDeadState(PigThrowBox pigThrowBox, EnemyStateMachine stateMachine)
    {
        _pigThrowBox = pigThrowBox;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pigThrowBox.Animation.PlayAnimation("Dead");
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}


