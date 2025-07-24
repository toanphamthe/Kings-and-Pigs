using UnityEngine;

public class ThrownBox : ThrownObject
{
    [Header("Thrown Box Settings")]
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private float _range;

    private void Update()
    {
        BreakBox();
    }

    private void BreakBox()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _range, collisionLayer);
        if (hit.collider != null)
        {
            ItemBox itemBox = GetComponent<ItemBox>();
            if (itemBox != null)
            {
                itemBox.TakeDamage();
            }

            if (hit.collider.CompareTag("Player"))
            {
                PlayerHit player = hit.collider.GetComponent<PlayerHit>();
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * _range);
    }
}
