using UnityEngine;

public abstract class ThrownObject : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected ObjectPool _objectPool;

    protected bool _hasExploded;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }
        
    public virtual void Initialize(ObjectPool pool)
    {
        _objectPool = pool;
    }

    protected virtual void OnEnable()
    {
        _hasExploded = false;
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }

    protected virtual void OnDisable()
    {
        CancelInvoke();
    }

    protected void PlayAnimation(string animation)
    {
        _animator.Play(animation);
    }

    public virtual void OnAnimationEnd()
    {
        PooledObject poolObj = GetComponent<PooledObject>();
        if (_objectPool != null && poolObj != null)
        {
            _objectPool.ReturnToPool(poolObj);
        }
    }
}
