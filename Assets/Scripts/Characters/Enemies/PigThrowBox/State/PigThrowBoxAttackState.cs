using UnityEngine;

public class PigThrowBoxAttackState : IState
{
    private PigThrowBox _pigThrowBox;
    private EnemyStateMachine _stateMachine;

    public PigThrowBoxAttackState(PigThrowBox pigThrowBox, EnemyStateMachine stateMachine)
    {
        _pigThrowBox = pigThrowBox;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pigThrowBox.Animation.PlayAnimation("ThrowingBox");
        _pigThrowBox.Attack.Attack();
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
