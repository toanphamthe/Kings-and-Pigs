using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStateMachine _stateMachine;
    public ICharacterAnimation Animation { get; private set; }
    public IPlayerInput Input { get; private set; }
    public IPlayerMovement Movement { get; private set; }
    public IPlayerAttackable Attack { get; private set; }
    public IPlayerDamageable Hit { get; private set; }
    public IPlayerHealth Health { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    private void Awake()
    {
        Animation = GetComponent<ICharacterAnimation>();
        Input = GetComponent<IPlayerInput>();
        Movement = GetComponent<IPlayerMovement>();
        Attack = GetComponent<IPlayerAttackable>();
        Hit = GetComponent<IPlayerDamageable>();
        Health = GetComponent<IPlayerHealth>();

        Rigidbody = GetComponent<Rigidbody2D>();

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

    public void PlaySFX(string name)
    {
        SoundManager.Instance.PlaySFX(name);
    }
}
