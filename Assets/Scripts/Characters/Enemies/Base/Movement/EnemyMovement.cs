using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemyMovement
{
    [SerializeField] protected Rigidbody2D _rb;

    [Header("Movement Settings")]
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected int _direction;
    [SerializeField] protected float _idleTimer;
    [SerializeField] protected float _idleDuration;

    [Header("Ground Check Settings")]
    [SerializeField] protected float _groundCheckRadius;
    [SerializeField] protected LayerMask _groundLayer;
    [SerializeField] protected bool _groundCheckGizmos;
    [SerializeField] protected Transform _groundCheckObj;
    [SerializeField] protected bool _isGrounded;

    [Header("Wall Check Settings")]
    [SerializeField] protected Transform _wallCheckObj;
    [SerializeField] protected float _wallCheckDistance;
    [SerializeField] protected bool _isHittingWall;
    [SerializeField] protected LayerMask _wallLayer;
    [SerializeField] protected bool _wallCheckGizmos;

    public float IdleTimer => _idleTimer;
    public float IdleDuration => _idleDuration;
    public bool IsGrounded => _isGrounded;
    public bool IsHittingWall => _isHittingWall;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        GroundCheck();
        WallCheck();
    }

    // This method is called by the enemy state machine to handle movement logic.
    protected virtual void GroundCheck()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckObj.position, _groundCheckRadius, _groundLayer);
    }

    protected virtual void WallCheck()
    {
        _isHittingWall = Physics2D.Raycast(_wallCheckObj.position, Vector2.right * _direction, _wallCheckDistance, _wallLayer);
    }

    // This method is called by the enemy state machine to handle movement logic.
    public virtual void HandleMovement()
    {
        if (!_isGrounded || _isHittingWall)
        {
            _direction *= -1;
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            return;
        }

        _rb.linearVelocity = new Vector2(_moveSpeed * _direction, _rb.linearVelocity.y);
    }

    // This method is called by the enemy state machine to flip the direction of the pig.
    public virtual void FlipDirection()
    {
        if (_direction == 1)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (_direction == -1)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    // This method is called by the enemy state machine to update the idle timer.
    public virtual void UpdateIdleTimer()
    {
        _idleTimer += Time.deltaTime;
    }

    // This method is called by the enemy state machine to reset the idle timer.
    public virtual void ResetIdleTimer()
    {
        _idleTimer = 0f;
    }

    // This method is called by the enemy state machine to draw gizmos in the editor for debugging purposes.
    protected virtual void OnDrawGizmos()
    {
        if (_groundCheckGizmos && _groundCheckObj != null)
        {
            Gizmos.color = _isGrounded ? Color.red : Color.green;
            Gizmos.DrawWireSphere(_groundCheckObj.position, _groundCheckRadius);
        }

        if (_wallCheckGizmos && _wallCheckObj != null)
        {
            Gizmos.color = _isHittingWall ? Color.red : Color.green;
            Gizmos.DrawLine(_wallCheckObj.position, _wallCheckObj.position + Vector3.right * _wallCheckDistance * _direction);
        }
    }
}
