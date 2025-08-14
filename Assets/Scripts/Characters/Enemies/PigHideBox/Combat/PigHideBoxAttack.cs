using UnityEngine;

public class PigHideBoxAttack : MonoBehaviour, IPigHideBoxAttack
{
    [Header("Jump Settings")]
    [SerializeField] private float _horizontalForce;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _facingDirection;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private bool _isFalling;

    [Header("Ground Check")]
    [SerializeField] private bool _isGrounded;
    [SerializeField] private Transform _groundCheckObj;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundLayer;

    [Header("Attack Settings")]
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _attackTimer;
    [SerializeField] private bool _isAttacking;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _playerLayers;
    [SerializeField] private int _playerDamage;

    [SerializeField] private Collider2D _enemyCollider;
    [SerializeField] private Collider2D _playerCollider;
    public bool IsAttacking => _isAttacking;

    public float AttackCooldown => _attackCooldown;

    public float AttackTimer => _attackTimer;

    public Collider2D HitPlayer { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyCollider = gameObject.GetComponent<Collider2D>();
    }

    private void Start()
    {
        _playerCollider = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Collider2D>();
    }

    private void Update()
    {
        GroundCheck();
        PlayerCheck();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHit>()?.TakeDamage(_playerDamage, transform.position);
        }
    }

    // This method is called by the enemy state machine when the enemy is in an attacking state.
    private void PlayerCheck()
    {
        HitPlayer = Physics2D.OverlapCircle(_attackPoint.position, _attackRange, _playerLayers);
        if (HitPlayer == null)
        {
            return;
        }
    }

    private void GroundCheck()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckObj.position, _groundCheckRadius, _groundLayer);
    }

    public void JumpOnPlayer()
    {
        FacingToPlayerDirection();

        Vector2 jumpDir = new Vector2(-_facingDirection * _horizontalForce, _jumpForce);
        Physics2D.IgnoreCollision(_enemyCollider, _playerCollider, true);
        _rb.linearVelocity = jumpDir;
    }

    private void FacingToPlayerDirection()
    {
        Player player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        if (player == null) return;
        _facingDirection = player.transform.position.x > transform.position.x ? -1 : 1;
        transform.localScale = new Vector3(_facingDirection, 1, 1);
    }

    public void HandleAttack()
    {
        if (_rb.linearVelocity.y < 0 && !_isGrounded)
        {
            GetComponent<ICharacterAnimation>()?.PlayAnimation("Fall");
            _isFalling = true;
        }

        if (_isFalling && _isGrounded)
        {
            GetComponent<ICharacterAnimation>()?.PlayAnimation("Ground");
            _isFalling = false;
            Physics2D.IgnoreCollision(_enemyCollider, _playerCollider, false);
        }
    }

    public void Attack()
    {
        _attackTimer = 0f;
        _isAttacking = true;
        JumpOnPlayer();
    }

    public void ResetAttack()
    {
        _isAttacking = false;
        HitPlayer = null;
    }

    public void UpdateAttackTimer()
    {
        _attackTimer += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheckObj.position, _groundCheckRadius);
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
