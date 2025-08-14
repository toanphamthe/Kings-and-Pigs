using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PigThrowBox : Enemy
{
    [SerializeField] private string _pigCurrentState;

    public PigThrowBoxIdleState IdleState { get; private set; }
    public PigThrowBoxAttackState AttackState { get; private set; }
    public PigThrowBoxIdleWithoutBoxState IdleWithoutBoxState { get; private set; }
    public PigThrowBoxHitState HitState { get; private set; }
    public PigThrowBoxDeadState DeadState { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    public ICharacterAnimation Animation { get; private set; }
    public IPigThrowBoxAttack Attack { get; private set; }
    public IEnemyHit Hit { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        IdleState = new PigThrowBoxIdleState(this, _stateMachine);
        AttackState = new PigThrowBoxAttackState(this, _stateMachine);
        IdleWithoutBoxState = new PigThrowBoxIdleWithoutBoxState(this, _stateMachine);
        HitState = new PigThrowBoxHitState(this, _stateMachine);
        DeadState = new PigThrowBoxDeadState(this, _stateMachine);

        Animation = GetComponent<ICharacterAnimation>();
        Attack = GetComponent<IPigThrowBoxAttack>();
        Hit = GetComponent<IEnemyHit>();

        Rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();
        _pigCurrentState = _stateMachine.CurrentState.GetType().Name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IPlayerDamageable>()?.TakeDamage(1, transform.position);
        }
    }

    protected override IState GetInitialState()
    {
        return IdleState;
    }

    public void OnThrowAnimationEnd()
    {
        _stateMachine.TransitionTo(IdleWithoutBoxState);
    }

    public void OnPickingBoxEnd()
    {
        _stateMachine.TransitionTo(AttackState);
    }
}
