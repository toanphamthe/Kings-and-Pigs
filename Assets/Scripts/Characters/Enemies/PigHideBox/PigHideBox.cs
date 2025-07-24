using UnityEngine;

public class PigHideBox : Enemy
{
    [SerializeField] private string _pigHideBoxCurrentState;

    public PigHideBoxAttackState AttackState { get; private set; }
    public PigHideBoxIdleState IdleState { get; private set; }
    public PigHideBoxLookingOutState LookingOutState { get; private set; }
    public PigHideBoxHitState HitState { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    public IEnemyMovement Movement { get; private set; }
    public ICharacterAnimation Animation { get; private set; }
    public IPigHideBoxAttack Attack { get; private set; }
    public IPigHideBoxLookingOut LookingOut { get; private set; }
    public IEnemyHit Hit { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        IdleState = new PigHideBoxIdleState(this, _stateMachine);
        LookingOutState = new PigHideBoxLookingOutState(this, _stateMachine);
        AttackState = new PigHideBoxAttackState(this, _stateMachine);
        HitState = new PigHideBoxHitState(this, _stateMachine);

        Movement = GetComponent<IEnemyMovement>();
        Animation = GetComponent<ICharacterAnimation>();
        Attack = GetComponent<IPigHideBoxAttack>();
        LookingOut = GetComponent<IPigHideBoxLookingOut>();
        Hit = GetComponent<IEnemyHit>();

        Rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();
        _pigHideBoxCurrentState = _stateMachine.CurrentState.GetType().Name;
    }

    protected override IState GetInitialState()
    {
        return IdleState;
    }

    public void OnLookingOutEnd()
    {
        if (Attack.HitPlayer)
        {
            _stateMachine.TransitionTo(AttackState);
        }
        else
        {
            _stateMachine.TransitionTo(IdleState);
        }
    }
}
