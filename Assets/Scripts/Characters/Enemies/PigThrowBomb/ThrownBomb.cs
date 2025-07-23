using UnityEngine;

public class ThrownBomb : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;

    [Header("Explosion Settings")]
    [SerializeField] private float _explosionMinDelay;
    [SerializeField] private float _explosionMaxDelay;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private bool _hasExploded;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PlayAnimation("On");
        float explosionDelay = Random.Range(_explosionMinDelay, _explosionMaxDelay);
        Invoke(nameof(Explode), explosionDelay);
    }

    private void PlayAnimation(string animation)
    {
        _animator.Play(animation);
    }

    private void Explode()
    {
        if (_hasExploded) return;
        PlayAnimation("Boom");
        _hasExploded = true;
        _rb.bodyType = RigidbodyType2D.Static;
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, _playerLayer);
        foreach (Collider2D player in hitPlayers)
        {
            PlayerHit playerHit = player.GetComponent<PlayerHit>();
            if (playerHit != null)
            {
                playerHit.TakeDamage(1, transform.position);
            }
        }
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
