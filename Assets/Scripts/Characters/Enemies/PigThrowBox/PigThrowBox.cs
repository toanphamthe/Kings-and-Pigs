using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PigThrowBox : Enemy
{
    [SerializeField] private string _pigCurrentState;

    public PigThrowBoxIdleState IdleState { get; private set; }
    public PigThrowBoxWalkState WalkState { get; private set; }
    public PigThrowBoxAttackState AttackState { get; private set; }
    public PigThrowBoxIdleWithoutBoxState IdleWithoutBoxState { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    public IEnemyMovement Movement { get; private set; }
    public ICharacterAnimation Animation { get; private set; }
    public IPigThrowBoxAttack Attack { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        IdleState = new PigThrowBoxIdleState(this, _stateMachine);
        WalkState = new PigThrowBoxWalkState(this, _stateMachine);
        AttackState = new PigThrowBoxAttackState(this, _stateMachine);
        IdleWithoutBoxState = new PigThrowBoxIdleWithoutBoxState(this, _stateMachine);

        Movement = GetComponent<IEnemyMovement>();
        Animation = GetComponent<ICharacterAnimation>();
        Attack = GetComponent<IPigThrowBoxAttack>();

        Rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();
        _pigCurrentState = _stateMachine.CurrentState.GetType().Name;
    }

    protected override IState GetInitialState()
    {
        return WalkState;
    }

    public void OnThrowAnimationEnd()
    {
        _stateMachine.TransitionTo(IdleWithoutBoxState);
    }

    public void OnPickingBoxEnd()
    {
        if (Attack.HitPlayer)
        {
            _stateMachine.TransitionTo(AttackState);
        }
        else
        {
            _stateMachine.TransitionTo(WalkState);
        }
    }
}
