using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
    private IPlayerInput _playerInput;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private Transform _groundCheckObj;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _jumpStartTime;
    [SerializeField] private float _minJumpDuration;
    [SerializeField] private bool _groundCheckGizmos;

    public bool IsGrounded => _isGrounded;
    public float JumpStartTime => _jumpStartTime;
    public float MinJumpDuration => _minJumpDuration;

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
    }

    // Handles the movement logic, setting the Rigidbody2D's velocity based on player input and flipping the sprite based on direction.
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

    // Handles the jump logic, applying a force to the Rigidbody2D if the player is grounded and the jump input is pressed.
    public void HandleJump()
    {
        _jumpStartTime = Time.time;
        if (_isGrounded && _playerInput.IsJumping)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
        }
    }

    // Checks if the player is grounded by using a circle overlap check.
    private void GroundCheck()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckObj.position, _groundCheckRadius, _groundLayer);
    }

    // Draw gizmos for ground check in the editor
    private void OnDrawGizmosSelected()
    {
        if (_groundCheckGizmos && _groundCheckObj != null)
        {
            Gizmos.color = _isGrounded ? Color.red : Color.green;
            Gizmos.DrawWireSphere(_groundCheckObj.position, _groundCheckRadius);
        }
    }
}
