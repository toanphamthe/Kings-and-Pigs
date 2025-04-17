using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
    private IPlayerInput _playerInput;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _moveSpeed = 5f;

    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private Transform _groundCheckObj;
    [SerializeField] private float _groundCheckRadius = 0.6f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _groundCheckGizmos;

    public bool IsGrounded => _isGrounded;  

    private void Awake()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (_playerInput == null)
        {
            _playerInput = GetComponent<IPlayerInput>();
        }
    }

    private void Update()
    {
        GroundCheck();
        HandleMovement();
        HandleJump();
    }

    public void HandleMovement()
    {
        _rb.linearVelocity = new Vector2(_playerInput.Horizontal * _moveSpeed, _rb.linearVelocity.y);

        if (_playerInput.Horizontal > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (_playerInput.Horizontal < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void HandleJump()
    {
        if (_isGrounded && _playerInput.IsJumping)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
        }
    }

    private void GroundCheck()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckObj.position, _groundCheckRadius, _groundLayer);
    }

    private void OnDrawGizmos()
    {
        if (_groundCheckGizmos && _groundCheckObj != null)
        {
            Gizmos.color = _isGrounded ? Color.red : Color.green;
            Gizmos.DrawWireSphere(_groundCheckObj.position, _groundCheckRadius);
        }
    }
}
