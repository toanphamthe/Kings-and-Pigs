using UnityEngine;

public class PigHideBoxHitState : IState
{
    private PigHideBox _pigHideBox;
    private EnemyStateMachine _stateMachine;

    public PigHideBoxHitState(PigHideBox pigHideBox, EnemyStateMachine stateMachine)
    {
        _pigHideBox = pigHideBox;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        Debug.Log("PigHideBoxHitState: Entering Hit State");
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
