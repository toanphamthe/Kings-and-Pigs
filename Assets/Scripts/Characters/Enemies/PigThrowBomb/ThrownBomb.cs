using UnityEngine;

public class ThrownBomb : ThrownObject
{

    [Header("Thrown Bomb Settings")]
    [SerializeField] private float _explosionMinDelay;
    [SerializeField] private float _explosionMaxDelay;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _playerLayer;

    protected override void OnEnable()
    {
        base.OnEnable();
        PlayAnimation("On");
        float explosionDelay = Random.Range(_explosionMinDelay, _explosionMaxDelay);
        Invoke(nameof(Explode), explosionDelay);
    }

    private void Explode()
    {
        if (_hasExploded) return;
        _hasExploded = true;
        PlayAnimation("Boom");
        _rb.bodyType = RigidbodyType2D.Static;

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, _playerLayer);
        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<PlayerHit>()?.TakeDamage(_damage, transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
