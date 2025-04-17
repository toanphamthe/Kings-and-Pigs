using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAnimation
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private string _currentAnimation;


    private void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    public void ChangeAnimation(string animationName)
    {
        if (_currentAnimation == animationName) return;

        _animator.Play(animationName);
        _currentAnimation = animationName;
    }
}
