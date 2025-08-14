using UnityEngine;

public class PigHideBoxAttackState : IState
{
    private PigHideBox _pigHideBox;
    private EnemyStateMachine _stateMachine;

    public PigHideBoxAttackState(PigHideBox pigHideBox, EnemyStateMachine stateMachine)
    {
        _pigHideBox = pigHideBox;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _pigHideBox.Animation.PlayAnimation("Jump");
        _pigHideBox.Attack.Attack();
    }

    public void Execute()
    {
        _pigHideBox.Attack.UpdateAttackTimer();
        _pigHideBox.Attack.HandleAttack();

        if (_pigHideBox.Attack.AttackTimer <= _pigHideBox.Attack.AttackCooldown) return;

        _stateMachine.TransitionTo(_pigHideBox.IdleState); // Stay in AttackState if player is hit
    }

    public void Exit()
    {
        _pigHideBox.Attack.ResetAttack(); // Reset the attack state after cooldown
    }
}
