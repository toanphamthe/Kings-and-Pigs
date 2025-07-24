using UnityEngine;

public class PigHideBoxIdleState : IState
{
    private PigHideBox _pigHideBox;
    private EnemyStateMachine _stateMachine;

    public PigHideBoxIdleState(PigHideBox pigHideBox, EnemyStateMachine stateMachine)
    {
        _pigHideBox = pigHideBox;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pigHideBox.Animation.PlayAnimation("Idle");
    }

    public void Execute()
    {
        _pigHideBox.LookingOut.UpdateLookingOutTimer();

        if (_pigHideBox.LookingOut.LookingOutTimer <= _pigHideBox.LookingOut.LookingOutCooldown) return;

        _stateMachine.TransitionTo(_pigHideBox.LookingOutState);
    }

    public void Exit()
    {

    }
}
