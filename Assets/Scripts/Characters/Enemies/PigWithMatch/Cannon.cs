using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Fire()
    {
        _animator.Play("Shoot");
    }

    public void Idle()
    {
        _animator.Play("Idle");
    }
}
