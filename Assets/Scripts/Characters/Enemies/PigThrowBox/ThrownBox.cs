using UnityEngine;

public class ThrownBox : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private float _range;
    [SerializeField] private bool _isBreakable;
    private void Update()
    {
        BreakBox();
    }

    private void BreakBox()
    {
        if (_isBreakable)
            return;
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
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * _range);
    }
}
