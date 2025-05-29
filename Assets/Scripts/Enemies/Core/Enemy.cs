using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private EnemyStateMachine _stateMachine;
    public IEnemyAnimation EnemyAnimation { get; private set; }

    protected virtual void Awake()
    {
        EnemyAnimation = GetComponent<IEnemyAnimation>();

        _stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        _stateMachine.Initialize(_stateMachine.IdleState);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public virtual void TakeDamage(float amount)
    {
        
    }
}
