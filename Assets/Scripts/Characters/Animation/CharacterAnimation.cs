using UnityEngine;

public class CharacterAnimation : MonoBehaviour, ICharacterAnimation
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _currentAnimation;


    private void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    public void PlayAnimation(string animationName)
    {
        if (_currentAnimation == animationName) return;

        _animator.Play(animationName);
        _currentAnimation = animationName;
    }
}
