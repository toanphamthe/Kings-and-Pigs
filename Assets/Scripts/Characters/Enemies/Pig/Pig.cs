using UnityEngine;

public class Pig : Enemy
{
    [SerializeField] private string _pigCurrentState;

    public PigIdleState IdleState { get; private set; }
    public PigWalkState WalkState { get; private set; }
    public PigHitState HitState { get; private set; }
    public PigDeadState DeadState { get; private set; }
    public PigAttackState AttackState { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    public IPigMovement Movement { get; private set; }
    public ICharacterAnimation Animation { get; private set; }
    public IEnemyAttack Attack { get; private set; }
    public IEnemyDamageable Hit {  get; private set; }

    protected override void Awake()
    {
        base.Awake();
        IdleState = new PigIdleState(this, _stateMachine);
        WalkState = new PigWalkState(this, _stateMachine);
        HitState = new PigHitState(this, _stateMachine);
        DeadState = new PigDeadState(this, _stateMachine);
        AttackState = new PigAttackState(this, _stateMachine);

        Movement = GetComponent<IPigMovement>();
        Animation = GetComponent<ICharacterAnimation>();
        Attack = GetComponent<IEnemyAttack>();
        Hit = GetComponent<IEnemyDamageable>();

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
}
