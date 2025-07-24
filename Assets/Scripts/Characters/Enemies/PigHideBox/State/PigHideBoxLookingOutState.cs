using UnityEngine;

public class PigHideBoxLookingOutState : IState
{
    private PigHideBox _pigHideBox;
    private EnemyStateMachine _stateMachine;
    private float _timer;

    public PigHideBoxLookingOutState(PigHideBox pigHideBox, EnemyStateMachine stateMachine)
    {
        _pigHideBox = pigHideBox;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pigHideBox.Animation.PlayAnimation("LookingOut");
        _timer = 0f;
    }

    public void Execute()
    {
        _timer += Time.deltaTime;

        if (!_pigHideBox.Attack.IsAttacking && _pigHideBox.Attack.HitPlayer && _timer > 1f)
        {
            _stateMachine.TransitionTo(_pigHideBox.AttackState); // Transition to AttackState if player is hit
        }
    }

    public void Exit()
    {

    }
}
