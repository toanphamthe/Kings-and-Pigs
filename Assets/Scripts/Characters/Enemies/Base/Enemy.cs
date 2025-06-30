using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected EnemyStateMachine _stateMachine;

    protected virtual void Awake()
    {
        _stateMachine = new EnemyStateMachine();
    }

    protected virtual void Start()
    {
        _stateMachine.Initialize(GetInitialState());
    }

    protected virtual void Update()
    {
        _stateMachine.Update();
    }

    protected virtual IState GetInitialState()
    {
        return null;
    }
}
