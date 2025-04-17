using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStateMachine _stateMachine;
    public IPlayerAnimation PlayerAnimation { get; private set; }
    public IPlayerInput PlayerInput { get; private set; }
    public IPlayerMovement PlayerMovement { get; private set; }
    public IPlayerAttack PlayerAttack { get; private set; }

    private void Awake()
    {
        PlayerAnimation = GetComponent<IPlayerAnimation>();
        PlayerInput = GetComponent<IPlayerInput>();
        PlayerMovement = GetComponent<IPlayerMovement>();
        PlayerAttack = GetComponent<IPlayerAttack>();

        _stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        _stateMachine.Initialize(_stateMachine.IdleState);
    }

    private void Update()
    {
        _stateMachine.Update();
    }
}
