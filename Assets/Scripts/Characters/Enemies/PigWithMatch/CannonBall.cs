using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private int _damage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _rb.AddForce(transform.right * _speed * -1, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHit playerHit = collision.GetComponent<PlayerHit>();
            if (playerHit != null)
            {
                playerHit.TakeDamage(_damage, transform.position);
            }
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Wall") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            BreakEffect();
        }
    }

    private void BreakEffect()
    {
        _animator.Play("Boom");
        _rb.bodyType = RigidbodyType2D.Static;    
    }

    public void DestroyCannonBall()
    {
        Destroy(gameObject);
    }
}
