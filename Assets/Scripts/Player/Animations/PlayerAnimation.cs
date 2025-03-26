using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAnimation
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private string _currentAnimation;

    void Start()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
    }

    public void HandleAnimation(IPlayerMovement playerMovement)
    {
        if (playerMovement.IsGrounded)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                ChangeAnimation("Run");
            }
            else
            {
                ChangeAnimation("Idle");
            }
        }
        else if (!playerMovement.IsGrounded)
        {
            if (_rb.linearVelocity.y > 0)
            {
                ChangeAnimation("Jump");
            }
            else if (_rb.linearVelocity.y < 0)
            {
                ChangeAnimation("Fall");
            }
        }
    }

    void ChangeAnimation(string newAnimation)
    {
        if (_currentAnimation == newAnimation) return;

        _animator.Play(newAnimation);
        _currentAnimation = newAnimation;
    }
}
