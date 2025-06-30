using UnityEngine;

public class PigMovement : MonoBehaviour, IPigMovement
{
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _groundCheckGizmos;
    [SerializeField] private Transform _groundCheckObj;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _direction;
    [SerializeField] private float _idleTimer;
    [SerializeField] private float _idleDuration;

    public float IdleTimer => _idleTimer;
    public float IdleDuration => _idleDuration;
    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GroundCheck();
    }

    // This method is called by the enemy state machine to handle movement logic.
    private void GroundCheck()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckObj.position, _groundCheckRadius, _groundLayer);
    }

    // This method is called by the enemy state machine to handle movement logic.
    public void HandleMovement()
    {
        if (!_isGrounded)
        {
            _direction *= -1;
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            return;
        }

        _rb.linearVelocity = new Vector2(_moveSpeed * _direction, _rb.linearVelocity.y);
    }

    // This method is called by the enemy state machine to flip the direction of the pig.
    public void FlipDirection()
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
    public void UpdateIdleTimer()
    {
        _idleTimer += Time.deltaTime;
    }

    // This method is called by the enemy state machine to reset the idle timer.
    public void ResetIdleTimer()
    {
        _idleTimer = 0f;
    }

    // This method is called by the enemy state machine to draw gizmos in the editor for debugging purposes.
    private void OnDrawGizmos()
    {
        if (_groundCheckGizmos && _groundCheckObj != null)
        {
            Gizmos.color = _isGrounded ? Color.red : Color.green;
            Gizmos.DrawWireSphere(_groundCheckObj.position, _groundCheckRadius);
        }
    }
}
