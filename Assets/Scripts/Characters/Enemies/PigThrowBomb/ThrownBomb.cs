using UnityEngine;

public class ThrownBomb : ThrownObject
{

    [Header("Thrown Bomb Settings")]
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _collisionLayer;

    protected override void OnEnable()
    {
        base.OnEnable();
        PlayAnimation("On");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_collisionLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerHit player = collision.GetComponent<PlayerHit>();
                if (player != null)
                {
                    player.TakeDamage(1, transform.position);
                }
            }
            Explode();
        }
    }

    private void Explode()
    {
        if (_hasExploded) return;
        _hasExploded = true;
        PlayAnimation("Boom");
        _rb.bodyType = RigidbodyType2D.Static;
    }

    public void ReturnToPool()
    {
        PooledObject poolObj = GetComponent<PooledObject>();
        if (_objectPool != null && poolObj != null)
        {
            _objectPool.ReturnToPool(poolObj);
        }
    }
}
