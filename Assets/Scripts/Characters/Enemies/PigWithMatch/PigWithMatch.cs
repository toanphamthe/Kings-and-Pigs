using UnityEngine;

public class PigWithMatch : Enemy
{
    [SerializeField] private string _pigCurrentState;
    [SerializeField] public Cannon Cannon;

    public PigWithMatchIdleState IdleState { get; private set; }
    public PigWithMatchAttackState AttackState { get; private set; }

    public ICharacterAnimation Animation { get; private set; }
    public IPigWithMatchAttack Attack { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        IdleState = new PigWithMatchIdleState(this, _stateMachine);
        AttackState = new PigWithMatchAttackState(this, _stateMachine);

        Animation = GetComponent<ICharacterAnimation>();
        Attack = GetComponent<IPigWithMatchAttack>();
    }

    protected override void Update()
    {
        base.Update();
        _pigCurrentState = _stateMachine.CurrentState.GetType().Name;
    }

    protected override IState GetInitialState()
    {
        return IdleState;
    }

    public void OnAttackStateEnd()
    {
        _stateMachine.TransitionTo(IdleState); // Transition back to idle state after attack
    }
}
