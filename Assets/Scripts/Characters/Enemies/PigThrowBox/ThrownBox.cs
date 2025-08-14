using UnityEngine;

public class ThrownBox : ThrownObject
{
    [Header("Thrown Box Settings")]
    [SerializeField] private LayerMask _collisionLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_collisionLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            ItemBox itemBox = GetComponent<ItemBox>();
            if (itemBox != null)
            {
                itemBox.TakeDamage();
            }

            if (collision.CompareTag("Player"))
            {
                PlayerHit player = collision.GetComponent<PlayerHit>();
                if (player != null)
                {
                    player.TakeDamage(1, transform.position);
                }
            }
            ExplodeAndReturnToPool();
        }
    }

    private void ExplodeAndReturnToPool()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        ReturnToPool();
    }
    
    private void ReturnToPool()
    {
        PooledObject poolObj = GetComponent<PooledObject>();
        if (_objectPool != null && poolObj != null)
        {
            _objectPool.ReturnToPool(poolObj);
        }
    }
}
